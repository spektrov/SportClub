using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using System.Linq;

namespace SportClub.ViewModel
{
    class TrainingInGroupTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public TrainingInGroup TrainingInGroupInfo { get; set; } = new TrainingInGroup();
        public TrainingInGroup SelectedTrainingInGroup { get; set; }

        public TrainingInGroupTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.TrainingInGroups.Load();
            Context.TrainingInGroups.Include(t => t.Client);
            Context.TrainingInGroups.Include(t => t.GroupTraining);
        }

        private RelayCommand _addTrainingInGroupCommand;
        private RelayCommand _updateTrainingInGroupCommand;
        private RelayCommand _deleteTrainingInGroupCommand;
        private RelayCommand _trainingInGroupGridSelectionChangedCommand;

        public ICommand AddTrainingInGroupCommand =>
            _addTrainingInGroupCommand ??
            (_addTrainingInGroupCommand = new RelayCommand(
                () =>
                {
                    Context.TrainingInGroups.Add(new TrainingInGroup
                    {
                        Client = TrainingInGroupInfo.Client,
                        GroupTraining = TrainingInGroupInfo.GroupTraining
                    });
                    Context.Trainings.Add(new Training
                    {
                        Client = TrainingInGroupInfo.Client,
                        TrainingDate = TrainingInGroupInfo.GroupTraining.Date
                    });

                    DecreaseTrainingCount();
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(TrainingInGroupInfo.Client, null)
                        || Equals(TrainingInGroupInfo.GroupTraining, null)
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

        public ICommand UpdateTrainingInGroupCommand =>
            _updateTrainingInGroupCommand ??
            (_updateTrainingInGroupCommand = new RelayCommand(
                () =>
                {
                    SelectedTrainingInGroup.Client = TrainingInGroupInfo.Client;
                    SelectedTrainingInGroup.GroupTraining = TrainingInGroupInfo.GroupTraining;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedTrainingInGroup == null) return false;
                    if (Equals(TrainingInGroupInfo.Client, null)
                        || Equals(TrainingInGroupInfo.GroupTraining, null)
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

        public ICommand DeleteTrainingInGroupCommand =>
           _deleteTrainingInGroupCommand ??
           (_deleteTrainingInGroupCommand = new RelayCommand(
               () =>
               {
                   Context.TrainingInGroups.Remove(SelectedTrainingInGroup);

                   var training = Context.Trainings.FirstOrDefault(t =>
                   t.Client.ClientId == TrainingInGroupInfo.Client.ClientId && t.TrainingDate == TrainingInGroupInfo.GroupTraining.Date);
                   Context.Trainings.Remove(training);

                   Context.SaveChanges();
               },
               () => SelectedTrainingInGroup != null));

        public ICommand TrainingInGroupGridSelectionChangedCommand =>
           _trainingInGroupGridSelectionChangedCommand ??
           (_trainingInGroupGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   TrainingInGroupInfo.Client = SelectedTrainingInGroup.Client;
                   TrainingInGroupInfo.GroupTraining = SelectedTrainingInGroup.GroupTraining;
               },
               () => SelectedTrainingInGroup != null));

        public bool OtherVerifies()
        {
            var query1 = $"SELECT * FROM Subscriptions " +
                $" WHERE ClientId = {TrainingInGroupInfo.Client.ClientId}" +
                $" AND ValidityDate >= GETDATE()" +
                $" AND VisitLeft > 0 " +
                $"AND GroupTrainingLeft > 0;";
            var subscriptions = Context.Subscriptions.SqlQuery(query1).ToList();

            var query2 = $"SELECT * FROM GroupTraining WHERE GroupTrainingId = {TrainingInGroupInfo.GroupTraining.GroupTrainingId} " +
                $"AND MaxAttenders > (SELECT COUNT(TrainingInGroupId) FROM TrainingInGroup WHERE GroupTrainingId = {TrainingInGroupInfo.GroupTraining.GroupTrainingId});";
            var groupTrainings = Context.GroupTrainings.SqlQuery(query2).ToList();

            var query3 = $"SELECT * FROM TrainingInGroup WHERE ClientId = {TrainingInGroupInfo.Client.ClientId} " +
               $"AND GroupTrainingId = {TrainingInGroupInfo.GroupTraining.GroupTrainingId}";
            var trainingInGroups = Context.TrainingInGroups.SqlQuery(query3).ToList();

            return subscriptions.Count > 0 && groupTrainings.Count > 0 && trainingInGroups.Count < 1;
        }

        public void DecreaseTrainingCount()
        {
            var subsc = Context.Subscriptions.FirstOrDefault(s => s.Client.ClientId == TrainingInGroupInfo.Client.ClientId);
            subsc.VisitLeft -= 1;
            subsc.GroupTrainingLeft -= 1;
            Context.SaveChanges();
        }
    }
}
