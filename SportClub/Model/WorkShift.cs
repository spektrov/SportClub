using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportClub.Annotations;

namespace SportClub.Model
{
    class WorkShift : INotifyPropertyChanged
    {
        private int _workShiftId;
        private DayOfWeekRussian _dayOfWeek;
        private DateTime _startHour;
        private DateTime _endHour;
        private IList<Schedule> _schedules;

        public int WorkShiftId
        {
            get => _workShiftId;
            set
            {
                if (value == _workShiftId) return;
                _workShiftId = value;
                OnPropertyChanged();
            }
        }

        public DayOfWeekRussian DayOfWeek
        {
            get => _dayOfWeek;
            set
            {
                if (value == _dayOfWeek) return;
                _dayOfWeek = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartHour
        {
            get => _startHour;
            set
            {
                if (value == _startHour) return;
                _startHour = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndHour
        {
            get => _endHour;
            set
            {
                if (value == _endHour) return;
                _endHour = value;
                OnPropertyChanged();
            }
        }

        public virtual IList<Schedule> Schedules
        {
            get => _schedules;
            set
            {
                if (value.Equals(_schedules)) return;
                _schedules = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WorkShift()
        {
            _schedules = new List<Schedule>();
        }

        public override string ToString()
        {
            return DayOfWeek.ToString() + " " + StartHour;
        }
    }
}
