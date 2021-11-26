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
    class Subscription : INotifyPropertyChanged
    {
        private int _clinetId;
        private int _tariffId;
        private int _visitLeft;
        private int _personalTrainingLeft;
        private int _groupTrainingLeft;
        private DateTime _buyDate;
        private DateTime _validityDate;

        private Client _client;
        private Tariff _tariff;

        public int ClientId
        {
            get => _clinetId;
            set
            {
                if (value == _clinetId) return;
                _clinetId = value;
                OnPropertyChanged();
            }
        }

        public int TariffId
        {
            get => _tariffId;
            set
            {
                if (value == _tariffId) return;
                _tariffId = value;
                OnPropertyChanged();
            }
        }

        public int VisitLeft
        {
            get => _visitLeft;
            set
            {
                if (value == _visitLeft) return;
                _visitLeft = value;
                OnPropertyChanged();
            }
        }

        public int PersonalTrainingLeft
        {
            get => _personalTrainingLeft;
            set
            {
                if (value == _personalTrainingLeft) return;
                _personalTrainingLeft = value;
                OnPropertyChanged();
            }
        }

        public int GroupTrainingLeft
        {
            get => _groupTrainingLeft;
            set
            {
                if (value == _groupTrainingLeft) return;
                _groupTrainingLeft = value;
                OnPropertyChanged();
            }
        }

        public DateTime BuyDate
        {
            get => _buyDate;
            set
            {
                if (value == _buyDate) return;
                _buyDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime ValidityDate
        {
            get => _validityDate;
            set
            {
                if (value == _validityDate) return;
                _validityDate = value;
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

        public virtual Tariff Tariff
        {
            get => _tariff;
            set
            {
                if (Equals(value, _tariff)) return;
                _tariff = value;
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
