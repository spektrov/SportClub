using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;


namespace SportClub.ViewModel
{
    class ScheduleTabViewModel : ViewModelBase
    {

        public SportClubContext Context { get; }
        public Schedule ScheduleInfo { get; set; } = new Schedule();
        public Schedule SelectedSchedule { get; set; }

        public ScheduleTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Schedules.Load();
            Context.Schedules.Include(s => s.WorkShift);
            Context.Schedules.Include(s => s.Trainer);
        }

        private RelayCommand _addScheduleCommand;
        private RelayCommand _updateScheduleCommand;
        private RelayCommand _deleteScheduleCommand;
        private RelayCommand _scheduleGridSelectionChangedCommand;

        public ICommand AddScheduleCommand =>
            _addScheduleCommand ??
            (_addScheduleCommand = new RelayCommand(
                () =>
                {
                    Context.Schedules.Add(new Schedule
                    {
                        Trainer = ScheduleInfo.Trainer,
                        WorkShift = ScheduleInfo.WorkShift
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(ScheduleInfo.Trainer, null)
                        || Equals(ScheduleInfo.WorkShift, null)
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

        public ICommand UpdateScheduleCommand =>
            _updateScheduleCommand ??
            (_updateScheduleCommand = new RelayCommand(
                () =>
                {
                    SelectedSchedule.Trainer = ScheduleInfo.Trainer;
                    SelectedSchedule.WorkShift = ScheduleInfo.WorkShift;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedSchedule == null) return false;
                    if (Equals(ScheduleInfo.Trainer, null)
                        || Equals(ScheduleInfo.WorkShift, null)
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

        public ICommand DeleteScheduleCommand =>
           _deleteScheduleCommand ??
           (_deleteScheduleCommand = new RelayCommand(
               () =>
               {
                   Context.Schedules.Remove(SelectedSchedule);
                   Context.SaveChanges();
               },
               () => SelectedSchedule != null));

        public ICommand ScheduleGridSelectionChangedCommand =>
           _scheduleGridSelectionChangedCommand ??
           (_scheduleGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   ScheduleInfo.Trainer = SelectedSchedule.Trainer;
                   ScheduleInfo.WorkShift = SelectedSchedule.WorkShift;
               },
               () => SelectedSchedule != null));

        public bool OtherVerifies()
        {
            var query1 = $"SELECT * FROM Schedule WHERE TrainerId = {ScheduleInfo.Trainer.TrainerId} " +
                $"AND WorkShiftId = {ScheduleInfo.WorkShift.WorkShiftId}";

            var schedules = Context.Schedules.SqlQuery(query1).ToListAsync();

            return schedules.Result.Count < 1;
        }
    }
}
