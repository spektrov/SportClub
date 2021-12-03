using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;

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
                    return true;
                }));

        public ICommand DeletePersonalTrainingCommand =>
           _deletePersonalTrainingCommand ??
           (_deletePersonalTrainingCommand = new RelayCommand(
               () =>
               {
                   Context.PersonalTrainings.Remove(SelectedPersonalTraining);
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
    }
}
