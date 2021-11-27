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
    class TrainingInGroup : INotifyPropertyChanged
    {
        private int _clientId;
        private Client _client;
        private int _groupTrainingId;
        private GroupTraining _groupTraining;

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

        public virtual GroupTraining GroupTraining
        {
            get => _groupTraining;
            set
            {
                if (Equals(value, _groupTraining)) return;
                _groupTraining = value;
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
