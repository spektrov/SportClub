using System;
using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;

namespace SportClub.ViewModel
{
    class GroupTrainingTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public GroupTraining GroupTrainingInfo { get; set; } = new GroupTraining() { Date = DateTime.Now, StartTime = DateTime.Now };
        public GroupTraining SelectedGroupTraining { get; set; }

        public GroupTrainingTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.GroupTrainings.Load();
            Context.GroupTrainings.Include(t => t.Trainer);
            Context.GroupTrainings.Include(t => t.Room);
            Context.GroupTrainings.Include(t => t.GroupTrainingType);
        }

        private RelayCommand _addGroupTrainingCommand;
        private RelayCommand _updateGroupTrainingCommand;
        private RelayCommand _deleteGroupTrainingCommand;
        private RelayCommand _groupTrainingGridSelectionChangedCommand;

        public ICommand AddGroupTrainingCommand =>
            _addGroupTrainingCommand ??
            (_addGroupTrainingCommand = new RelayCommand(
                () =>
                {
                    Context.GroupTrainings.Add(new GroupTraining
                    {
                        Trainer = GroupTrainingInfo.Trainer,
                        Room = GroupTrainingInfo.Room,
                        GroupTrainingType = GroupTrainingInfo.GroupTrainingType,
                        Date = GroupTrainingInfo.Date,
                        StartTime = GroupTrainingInfo.StartTime
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(GroupTrainingInfo.Trainer, null)
                        || Equals(GroupTrainingInfo.Room, null)
                        || Equals(GroupTrainingInfo.GroupTrainingType, null)
                        || Equals(GroupTrainingInfo.Date, null)
                        || Equals(GroupTrainingInfo.StartTime, null)
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateGroupTrainingCommand =>
            _updateGroupTrainingCommand ??
            (_updateGroupTrainingCommand = new RelayCommand(
                () =>
                {
                    SelectedGroupTraining.Trainer = GroupTrainingInfo.Trainer;
                    SelectedGroupTraining.Room = GroupTrainingInfo.Room;
                    SelectedGroupTraining.GroupTrainingType = GroupTrainingInfo.GroupTrainingType;
                    SelectedGroupTraining.Date = GroupTrainingInfo.Date;
                    SelectedGroupTraining.StartTime = GroupTrainingInfo.StartTime;
                  
                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedGroupTraining == null) return false;
                    if (Equals(GroupTrainingInfo.Trainer, null)
                        || Equals(GroupTrainingInfo.Room, null)
                        || Equals(GroupTrainingInfo.GroupTrainingType, null)
                        || Equals(GroupTrainingInfo.Date, null)
                        || Equals(GroupTrainingInfo.StartTime, null)
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteGroupTrainingCommand =>
           _deleteGroupTrainingCommand ??
           (_deleteGroupTrainingCommand = new RelayCommand(
               () =>
               {
                   Context.GroupTrainings.Remove(SelectedGroupTraining);
                   Context.SaveChanges();
               },
               () => SelectedGroupTraining != null));

        public ICommand GroupTrainingGridSelectionChangedCommand =>
           _groupTrainingGridSelectionChangedCommand ??
           (_groupTrainingGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   GroupTrainingInfo.Trainer = SelectedGroupTraining.Trainer;
                   GroupTrainingInfo.Room = SelectedGroupTraining.Room;
                   GroupTrainingInfo.GroupTrainingType = SelectedGroupTraining.GroupTrainingType;
                   GroupTrainingInfo.Date = SelectedGroupTraining.Date;
                   GroupTrainingInfo.StartTime = SelectedGroupTraining.StartTime;
               },
               () => SelectedGroupTraining != null));
    }

}
