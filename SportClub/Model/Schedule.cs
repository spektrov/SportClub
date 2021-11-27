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
    class Schedule : INotifyPropertyChanged
    {
        private int _trainerId;
        private Trainer _trainer;
        private int _workShiftId;
        private WorkShift _workShift;

        public int TrainerId
        {
            get => _trainerId;
            set
            {
                if (value == _trainerId) return;
                _trainerId = value;
                OnPropertyChanged();
            }
        }

        public virtual Trainer Trainer
        {
            get => _trainer;
            set
            {
                if (Equals(value, _trainer)) return;
                _trainer = value;
                OnPropertyChanged();
            }
        }

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

        public virtual WorkShift WorkShift
        {
            get => _workShift;
            set
            {
                if (Equals(value, _workShift)) return;
                _workShift = value;
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
