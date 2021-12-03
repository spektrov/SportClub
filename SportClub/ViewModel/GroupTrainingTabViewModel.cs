using System;
using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using System.Linq;

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
                        StartTime = GroupTrainingInfo.StartTime,
                        MaxAttenders = GroupTrainingInfo.MaxAttenders
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
                        || GroupTrainingInfo.MaxAttenders <= 0
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
                    SelectedGroupTraining.MaxAttenders = GroupTrainingInfo.MaxAttenders;

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
                        || GroupTrainingInfo.MaxAttenders <= 0
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
                   GroupTrainingInfo.MaxAttenders = SelectedGroupTraining.MaxAttenders;
               },
               () => SelectedGroupTraining != null));


        public bool OtherVerifies()
        {
            var date = GroupTrainingInfo.Date.Year + "-" + GroupTrainingInfo.Date.Month + "-" + GroupTrainingInfo.Date.Day;
            var query1 = $"SELECT * FROM GroupTraining" +
                $" WHERE (RoomId = {GroupTrainingInfo.Room.RoomId} OR TrainerId = {GroupTrainingInfo.Trainer.TrainerId})" +
                $" AND CAST(Date AS DATE) = CAST('{date}' AS DATE)" +
                $" AND ((CAST(StartTime AS TIME) > CAST('{GroupTrainingInfo.StartTime}' AS TIME)" +
                $" AND CAST(StartTime AS TIME) <= CAST('{GroupTrainingInfo.StartTime.AddMinutes(59).AddSeconds(59)}' AS TIME))" +
                $" OR ((CAST(DATEADD(HH, 1, StartTime) AS TIME) > CAST('{GroupTrainingInfo.StartTime}' AS TIME)" +
                $" AND CAST(DATEADD(HH, 1, StartTime) AS TIME) <= CAST('{GroupTrainingInfo.StartTime.AddHours(1)}' AS TIME))));";
            var groupTraining = Context.GroupTrainings.SqlQuery(query1).ToList();

            var dayOfWeek = (int)GroupTrainingInfo.Date.DayOfWeek == 7 ? 0 : (int)GroupTrainingInfo.Date.DayOfWeek;
            var query2 = $"SELECT * FROM Trainers WHERE TrainerId NOT IN (" +
               $"SELECT Schedule.TrainerId FROM Schedule, WorkShifts" +
               $" WHERE Schedule.WorkShiftId = WorkShifts.WorkShiftId AND DayOfWeek = {dayOfWeek} " +
               $"AND CAST(StartHour AS TIME) <= CAST('{GroupTrainingInfo.StartTime}' AS TIME)" +
               $" AND CAST(EndHour AS TIME) >= CAST('{GroupTrainingInfo.StartTime.AddHours(1)}' AS TIME) ) " +
               $"AND TrainerId = {GroupTrainingInfo.Trainer.TrainerId}; ";
            var trainers = Context.Trainers.SqlQuery(query2).ToList();

            return groupTraining.Count < 1 && trainers.Count > 0;
        }
    }
}
