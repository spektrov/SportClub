using System;
using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;

namespace SportClub.ViewModel
{
    class WorkShiftTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public WorkShift WorkShiftInfo { get; set; } = new WorkShift();
        public WorkShift SelectedWorkShift { get; set; }

        public Array DayOfWeekRussians { get => Enum.GetValues(typeof(DayOfWeekRussian)); }

        public WorkShiftTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.WorkShifts.Load();
        }

        private RelayCommand _addWorkShiftCommand;
        private RelayCommand _updateWorkShiftCommand;
        private RelayCommand _deleteWorkShiftCommand;
        private RelayCommand _workShiftGridSelectionChangedCommand;

        public ICommand AddWorkShiftCommand =>
            _addWorkShiftCommand ??
            (_addWorkShiftCommand = new RelayCommand(
                () =>
                {
                    Context.WorkShifts.Add(new WorkShift
                    {
                        DayOfWeek = WorkShiftInfo.DayOfWeek,
                        StartHour = WorkShiftInfo.StartHour,
                        EndHour = WorkShiftInfo.EndHour
                });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (Equals(WorkShiftInfo.DayOfWeek, null)
                        || Equals(WorkShiftInfo.StartHour, null)
                        || Equals(WorkShiftInfo.EndHour, null)
                        || WorkShiftInfo.StartHour >= WorkShiftInfo.EndHour
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateWorkShiftCommand =>
           _updateWorkShiftCommand ??
           (_updateWorkShiftCommand = new RelayCommand(
               () =>
               {
                   SelectedWorkShift.DayOfWeek = WorkShiftInfo.DayOfWeek;
                   SelectedWorkShift.StartHour = WorkShiftInfo.StartHour;
                   SelectedWorkShift.EndHour = WorkShiftInfo.EndHour;

                   Context.SaveChanges();
               },
               () =>
               {
                   if (SelectedWorkShift == null) return false;
                   if (Equals(WorkShiftInfo.DayOfWeek, null)
                        || Equals(WorkShiftInfo.StartHour, null)
                        || Equals(WorkShiftInfo.EndHour, null)
                        || WorkShiftInfo.StartHour >= WorkShiftInfo.EndHour
                        )
                   {
                       return false;
                   }
                   return true;
               }));

        public ICommand DeleteWorkShiftCommand =>
           _deleteWorkShiftCommand ??
           (_deleteWorkShiftCommand = new RelayCommand(
               () =>
               {
                   Context.WorkShifts.Remove(SelectedWorkShift);
                   Context.SaveChanges();
               },
               () => SelectedWorkShift != null));

        public ICommand WorkShiftGridSelectionChangedCommand =>
           _workShiftGridSelectionChangedCommand ??
           (_workShiftGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   WorkShiftInfo.DayOfWeek = SelectedWorkShift.DayOfWeek;
                   WorkShiftInfo.StartHour = SelectedWorkShift.StartHour;
                   WorkShiftInfo.EndHour = SelectedWorkShift.EndHour;
               },
               () => SelectedWorkShift != null));
    }
}
