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
    class Training : INotifyPropertyChanged
    {
        private int _trainingId;
        private int _clientId;
        private DateTime _trainingDate;

        private Client _client;

        public int TrainingId
        {
            get => _trainingId;
            set
            {
                if (value == _trainingId) return;
                _trainingId = value;
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
