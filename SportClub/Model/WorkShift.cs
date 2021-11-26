using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportClub.Annotations;

namespace SportClub.Model
{
    class WorkShift : INotifyPropertyChanged
    {
        private int _workShiftId;
        private DayOfWeek _dayOfWeek;
        private DateTime _startHour;
        private DateTime _endHour;

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

        public DayOfWeek DayOfWeek
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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
