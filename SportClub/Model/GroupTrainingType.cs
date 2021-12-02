using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SportClub.Annotations;

namespace SportClub.Model
{
    class GroupTrainingType : INotifyPropertyChanged
    {
        private int _groupTrainingTypeId;
        private string _groupTrainingTypeName;

        private IList<GroupTraining> _groupTrainings;

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

        public string GroupTrainingTypeName
        {
            get => _groupTrainingTypeName;
            set
            {
                if (value == _groupTrainingTypeName) return;
                _groupTrainingTypeName = value;
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

        public GroupTrainingType()
        {
            _groupTrainings = new List<GroupTraining>();
        }

        public override string ToString()
        {
            return GroupTrainingTypeName.ToString();
        }
    }
}
