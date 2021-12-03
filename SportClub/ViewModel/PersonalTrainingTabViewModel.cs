using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using System.Linq;

namespace SportClub.ViewModel
{
    class PersonalTrainingTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public PersonalTraining PersonalTrainingInfo { get; set; } = new PersonalTraining() { TrainingDate = System.DateTime.Now };
        public PersonalTraining SelectedPersonalTraining { get; set; }

        public PersonalTrainingTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.PersonalTrainings.Load();
            Context.PersonalTrainings.Include(t => t.Client);
            Context.PersonalTrainings.Include(t => t.Trainer);
            Context.Subscriptions.Load();
        }

        private RelayCommand _addPersonalTrainingCommand;
        private RelayCommand _updatePersonalTrainingCommand;
        private RelayCommand _deletePersonalTrainingCommand;
        private RelayCommand _personalTrainingsGridSelectionChangedCommand;

        public ICommand AddPersonalTrainingCommand =>
            _addPersonalTrainingCommand ??
            (_addPersonalTrainingCommand = new RelayCommand(
                () =>
                {
                    Context.PersonalTrainings.Add(new PersonalTraining
                    {
                        Client = PersonalTrainingInfo.Client,
                        Trainer = PersonalTrainingInfo.Trainer,
                        TrainingDate = PersonalTrainingInfo.TrainingDate,
                        StartTime = PersonalTrainingInfo.StartTime
                    });
                    Context.Trainings.Add(new Training
                    {
                        Client = PersonalTrainingInfo.Client,
                        TrainingDate = PersonalTrainingInfo.TrainingDate
                    });

                    DecreaseTrainingCount();
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(PersonalTrainingInfo.Client, null)
                        || Equals(PersonalTrainingInfo.Trainer, null)
                        || Equals(PersonalTrainingInfo.TrainingDate, null)
                        || Equals(PersonalTrainingInfo.StartTime, null)
                        )
                    {
                        return false;
                    }
                    if (!OtherVerifies())
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdatePersonalTrainingCommand =>
            _updatePersonalTrainingCommand ??
            (_updatePersonalTrainingCommand = new RelayCommand(
                () =>
                {
                    SelectedPersonalTraining.Client = PersonalTrainingInfo.Client;
                    SelectedPersonalTraining.Trainer = PersonalTrainingInfo.Trainer;
                    SelectedPersonalTraining.TrainingDate = PersonalTrainingInfo.TrainingDate;
                    SelectedPersonalTraining.StartTime = PersonalTrainingInfo.StartTime;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedPersonalTraining == null) return false;
                    if (Equals(PersonalTrainingInfo.Client, null)
                        || Equals(PersonalTrainingInfo.Trainer, null)
                        || Equals(PersonalTrainingInfo.TrainingDate, null)
                        || Equals(PersonalTrainingInfo.StartTime, null)
                        )
                    {
                        return false;
                    }
                    if (!OtherVerifies())
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeletePersonalTrainingCommand =>
           _deletePersonalTrainingCommand ??
           (_deletePersonalTrainingCommand = new RelayCommand(
               () =>
               {
                   Context.PersonalTrainings.Remove(SelectedPersonalTraining);

                   var training = Context.Trainings.FirstOrDefault(t => 
                   t.Client.ClientId == PersonalTrainingInfo.Client.ClientId && t.TrainingDate == PersonalTrainingInfo.TrainingDate);
                   Context.Trainings.Remove(training);

                   Context.SaveChanges();
               },
               () => SelectedPersonalTraining != null));

        public ICommand PersonalTrainingsGridSelectionChangedCommand =>
           _personalTrainingsGridSelectionChangedCommand ??
           (_personalTrainingsGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   PersonalTrainingInfo.Client = SelectedPersonalTraining.Client;
                   PersonalTrainingInfo.Trainer = SelectedPersonalTraining.Trainer;
                   PersonalTrainingInfo.TrainingDate = SelectedPersonalTraining.TrainingDate;
                   PersonalTrainingInfo.StartTime = SelectedPersonalTraining.StartTime;
               },
               () => SelectedPersonalTraining != null));

        public bool OtherVerifies()
        {
            var query1 = $"SELECT * FROM Subscriptions " +
                $" WHERE ClientId = {PersonalTrainingInfo.Client.ClientId}" +
                $" AND ValidityDate >= GETDATE()" +
                $" AND VisitLeft > 0 " +
                $"AND PersonalTrainingLeft > 0;";
            var subscriptions = Context.Subscriptions.SqlQuery(query1).ToList();

            var dayOfWeek = (int)PersonalTrainingInfo.TrainingDate.DayOfWeek == 7 ? 0 : (int)PersonalTrainingInfo.TrainingDate.DayOfWeek;
            var query2 = $"SELECT * FROM Trainers WHERE TrainerId IN (" +
                $"SELECT Schedule.TrainerId FROM Schedule, WorkShifts" +
                $" WHERE Schedule.WorkShiftId = WorkShifts.WorkShiftId AND DayOfWeek = {dayOfWeek} " +
                $"AND CAST(StartHour AS TIME) <= CAST('{PersonalTrainingInfo.StartTime}' AS TIME)" +
                $" AND CAST(EndHour AS TIME) >= CAST('{PersonalTrainingInfo.StartTime.AddHours(1)}' AS TIME) ) " +
                $"AND TrainerId = {PersonalTrainingInfo.Trainer.TrainerId}; ";
            var trainers = Context.Trainers.SqlQuery(query2).ToList();

            var date = PersonalTrainingInfo.TrainingDate.Year + "-" + PersonalTrainingInfo.TrainingDate.Month + "-" + PersonalTrainingInfo.TrainingDate.Day;
            var query3 = $"SELECT * FROM PersonalTrainings" +
                $" WHERE TrainerId =  {PersonalTrainingInfo.Trainer.TrainerId}" +
                $" AND CAST(TrainingDate AS DATE) = CAST('{date}' AS DATE)" +
                $" AND ((CAST(StartTime AS TIME) > CAST('{PersonalTrainingInfo.StartTime}' AS TIME)" +
                $"       AND CAST(StartTime AS TIME) <= CAST('{PersonalTrainingInfo.StartTime.AddMinutes(59).AddSeconds(59)}' AS TIME)) " +
                $"  OR ((CAST(DATEADD(HH, 1, StartTime) AS TIME) > CAST('{PersonalTrainingInfo.StartTime}' AS TIME)" +
                $"       AND CAST(DATEADD(HH, 1, StartTime) AS TIME) <= CAST('{PersonalTrainingInfo.StartTime.AddHours(1)}' AS TIME))));";
                
            var personalTrainings = Context.PersonalTrainings.SqlQuery(query3).ToList();

            return subscriptions.Count > 0 && trainers.Count > 0 && personalTrainings.Count < 1;
        }

        public void DecreaseTrainingCount()
        {
            var subsc = Context.Subscriptions.FirstOrDefault(s => s.Client.ClientId == PersonalTrainingInfo.Client.ClientId);
            subsc.VisitLeft -= 1;
            subsc.PersonalTrainingLeft -= 1;
            Context.SaveChanges();
        }
    }
}
