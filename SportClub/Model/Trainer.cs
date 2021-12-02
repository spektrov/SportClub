using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportClub.Annotations;

namespace SportClub.Model
{
    class Trainer : INotifyPropertyChanged
    {
        private int _trainerId;
        private string _lastName;
        private string _firstName;
        private DateTime? _birthDate;
        private Genders _gender;
        private string _phoneNumber;
        private string _email;
        private DateTime _applyDate;
        private string _education;
        private string _sportAchivements;
        private int? _experience;
        private decimal? _salary;

        private IList<PersonalTraining> _personalTrainings;
        private IList<Schedule> _schedules;
        private IList<GroupTraining> _groupTrainings;

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

        public DateTime ApplyDate
        {
            get => _applyDate;
            set
            {
                if (value.Equals(_applyDate)) return;
                _applyDate = value;
                OnPropertyChanged();
            }
        }

        public string Education
        {
            get => _education;
            set
            {
                if (value == _education) return;
                _education = value;
                OnPropertyChanged();
            }
        }

        public string SportAchivements
        {
            get => _sportAchivements;
            set
            {
                if (value == _sportAchivements) return;
                _sportAchivements = value;
                OnPropertyChanged();
            }
        }

        public int? Experience
        {
            get => _experience;
            set
            {
                if (value == _experience) return;
                _experience = value;
                OnPropertyChanged();
            }
        }

        public decimal? Salary
        {
            get => _salary;
            set
            {
                if (value == _salary) return;
                _salary = value;
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

        public virtual IList<GroupTraining> GroupTrainings
        {
            get => _groupTrainings;
            set
            {
                if (value.Equals(_groupTrainings)) return;
                _groupTrainings = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Trainer()
        {
            _personalTrainings = new List<PersonalTraining>();
            _schedules = new List<Schedule>();
            _groupTrainings = new List<GroupTraining>();
        }
    }
}
