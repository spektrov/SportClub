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
    class PersonalTraining : INotifyPropertyChanged
    {
        private int _personalTrainingId;
        private int _trainerId;
        private int _clientId;
        private DateTime _trainingDate;
        private DateTime _startTime;

        private Client _client;
        private Trainer _trainer;

        public int PersonalTrainingId
        {
            get => _personalTrainingId;
            set
            {
                if (value == _personalTrainingId) return;
                _personalTrainingId = value;
                OnPropertyChanged();
            }
        }

        public int ClientId
        {
            get => _clientId;
            set
            {
                if (value == _clientId) return;
                _clientId = value;
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

        public DateTime TrainingDate
        {
            get => _trainingDate;
            set
            {
                if (value.Equals(_trainingDate)) return;
                _trainingDate = value;
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

        public virtual Client Client
        {
            get => _client;
            set
            {
                if (Equals(value, _client)) return;
                _client = value;
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


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
