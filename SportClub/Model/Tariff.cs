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
    class Tariff : INotifyPropertyChanged
    {
        private int _tariffId;
        private decimal _price;
        private int _visitNumber;
        private int _personalTraining;
        private int _groupTraining;
        private string _additionalInformation;

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

        public decimal Price
        {
            get => _price;
            set
            {
                if (value == _price) return;
                _price = value;
                OnPropertyChanged();
            }
        }

        public int VisitNumber
        {
            get => _visitNumber;
            set
            {
                if (value == _visitNumber) return;
                _visitNumber = value;
                OnPropertyChanged();
            }
        }

        public int PersonalTraining
        {
            get => _personalTraining;
            set
            {
                if (value == _personalTraining) return;
                _personalTraining = value;
                OnPropertyChanged();
            }
        }

        public int GroupTraining
        {
            get => _groupTraining;
            set
            {
                if (value == _groupTraining) return;
                _groupTraining = value;
                OnPropertyChanged();
            }
        }

        public string AdditionalInformation
        {
            get => _additionalInformation;
            set
            {
                if (value == _additionalInformation) return;
                _additionalInformation = value;
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
