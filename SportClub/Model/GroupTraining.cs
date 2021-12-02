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
    class GroupTraining : INotifyPropertyChanged
    {
        private int _groupTrainingId;
        private int _trainerId;
        private Trainer _trainer;
        private int _roomId;
        private Room _room;
        private int _groupTrainingTypeId;
        private GroupTrainingType _groupTrainingType;
        private DateTime _date;
        private DateTime _startTime;

        private IList<TrainingInGroup> _trainingInGroups;

        public int GroupTrainingId
        {
            get => _groupTrainingId;
            set
            {
                if (value == _groupTrainingId) return;
                _groupTrainingId = value;
                OnPropertyChanged();
            }
        }

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

        public int RoomId
        {
            get => _roomId;
            set
            {
                if (value == _roomId) return;
                _roomId = value;
                OnPropertyChanged();
            }
        }

        public virtual Room Room
        {
            get => _room;
            set
            {
                if (Equals(value, _room)) return;
                _room = value;
                OnPropertyChanged();
            }
        }

        public int GroupTrainingTypeId
        {
            get => _groupTrainingTypeId;
            set
            {
                if (value == _groupTrainingTypeId) return;
                _groupTrainingTypeId = value;
                OnPropertyChanged();
            }
        }

        public virtual GroupTrainingType GroupTrainingType
        {
            get => _groupTrainingType;
            set
            {
                if (Equals(value, _groupTrainingType)) return;
                _groupTrainingType = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                if (value.Equals(_date)) return;
                _date = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                if (value.Equals(_startTime)) return;
                _startTime = value;
                OnPropertyChanged();
            }
        }

        public virtual IList<TrainingInGroup> TrainingInGroups
        {
            get => _trainingInGroups;
            set
            {
                if (Equals(value, _trainingInGroups)) return;
                _trainingInGroups = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GroupTraining()
        {
            _trainingInGroups = new List<TrainingInGroup>();
        }

        public override string ToString()
        {
            return Date.Day.ToString() + "." + Date.Month.ToString() + " "
                + StartTime.Hour.ToString() + ":" + StartTime.Minute.ToString() + " " + GroupTrainingType.GroupTrainingTypeName;
        }
    }
}
