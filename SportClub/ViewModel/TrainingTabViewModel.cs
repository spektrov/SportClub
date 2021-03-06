using System;
using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using System.Data.SqlTypes;
using System.Linq;

namespace SportClub.ViewModel
{
    class TrainingTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public Training TrainingInfo { get; set; } = new Training() { TrainingDate = System.DateTime.Now};
        public Training SelectedTraining { get; set; }

        public TrainingTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Trainings.Load();
            Context.Trainings.Include(t => t.Client);
            Context.Subscriptions.Load();
        }

        private RelayCommand _addTrainingCommand;
        private RelayCommand _updateTrainingCommand;
        private RelayCommand _deleteTrainingCommand;
        private RelayCommand _trainingsGridSelectionChangedCommand;

        public ICommand AddTrainingCommand =>
            _addTrainingCommand ??
            (_addTrainingCommand = new RelayCommand(
                () =>
                {
                    Context.Trainings.Add(new Training
                    {
                        Client = TrainingInfo.Client,
                        TrainingDate = TrainingInfo.TrainingDate
                    });

                    DecreaseTrainingCount();
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(TrainingInfo.Client, null)
                        || Equals(TrainingInfo.TrainingDate, null)
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

        public ICommand UpdateTrainingCommand =>
            _updateTrainingCommand ??
            (_updateTrainingCommand = new RelayCommand(
                () =>
                {
                    SelectedTraining.Client = TrainingInfo.Client;
                    SelectedTraining.TrainingDate = TrainingInfo.TrainingDate;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedTraining == null) return false;
                    if (Equals(TrainingInfo.Client, null)
                        || Equals(TrainingInfo.TrainingDate, null)
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

        public ICommand DeleteTrainingCommand =>
           _deleteTrainingCommand ??
           (_deleteTrainingCommand = new RelayCommand(
               () =>
               {
                   Context.Trainings.Remove(SelectedTraining);
                   Context.SaveChanges();
               },
               () => SelectedTraining != null));

        public ICommand TrainingsGridSelectionChangedCommand =>
           _trainingsGridSelectionChangedCommand ??
           (_trainingsGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   TrainingInfo.Client = SelectedTraining.Client;
                   TrainingInfo.TrainingDate = SelectedTraining.TrainingDate;
               },
               () => SelectedTraining != null));


        public bool OtherVerifies()
        {
            var query1 = $"SELECT * FROM Subscriptions " +
                $" WHERE ClientId = {TrainingInfo.Client.ClientId}" +
                $" AND ValidityDate > GETDATE()" +
                $" AND VisitLeft > 0";

            var subscriptions = Context.Subscriptions.SqlQuery(query1).ToListAsync();

            return subscriptions.Result.Count > 0;
        }

        public void DecreaseTrainingCount()
        {
            var subsc = Context.Subscriptions.FirstOrDefault(s => s.Client.ClientId == TrainingInfo.Client.ClientId);
            subsc.VisitLeft -= 1;
            Context.SaveChanges();
        }
    }
}
