using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;

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
                    return true;
                }));

        public ICommand DeleteTrainingInGroupCommand =>
           _deleteTrainingInGroupCommand ??
           (_deleteTrainingInGroupCommand = new RelayCommand(
               () =>
               {
                   Context.TrainingInGroups.Remove(SelectedTrainingInGroup);
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
    }
}
