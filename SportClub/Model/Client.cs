using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportClub.Annotations;


namespace SportClub.Model
{
    class Client : INotifyPropertyChanged
    {
        private int _clientId;
        private string _lastName;
        private string _firstName;
        private DateTime? _birthDate;
        private Genders _gender;
        private string _phoneNumber;
        private string _email;
        private DateTime _registrationDate;

        private IList<Subscription> _subscriptions;
        private IList<Training> _trainings;
        private IList<PersonalTraining> _personalTrainings;
        private IList<TrainingInGroup> _trainingInGroups;

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

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == _lastName) return;
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value == _firstName) return;
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                if (value.Equals(_birthDate)) return;
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public Genders Gender
        {
            get => _gender;
            set
            {
                if (value == _gender) return;
                _gender = value;
                OnPropertyChanged();
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (value == _phoneNumber) return;
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value == _email) return;
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime RegistrationDate
        {
            get => _registrationDate;
            set
            {
                if (value.Equals(_registrationDate)) return;
                _registrationDate = value;
                OnPropertyChanged();
            }
        }

        public virtual IList<Subscription> Subscriptions
        {
            get => _subscriptions;
            set
            {
                if (value.Equals(_subscriptions)) return;
                _subscriptions = value;
                OnPropertyChanged();
            }
        }

        public virtual IList<Training> Trainings
        {
            get => _trainings;
            set
            {
                if (Equals(value, _trainings)) return;
                _trainings = value;
                OnPropertyChanged();
            }
        }

        public virtual IList<PersonalTraining> PersonalTrainings
        {
            get => _personalTrainings;
            set
            {
                if (Equals(value, _personalTrainings)) return;
                _personalTrainings = value;
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

        public Client()
        {
            _trainings = new List<Training>();
            _personalTrainings = new List<PersonalTraining>();
            _trainingInGroups = new List<TrainingInGroup>();
            _subscriptions = new List<Subscription>();
        }

        public override string ToString()
        {
            return ClientId.ToString();
        }
    }
}
