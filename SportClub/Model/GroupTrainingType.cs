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
    class GroupTrainingType : INotifyPropertyChanged
    {
        private int _groupTrainingTypeId;
        private string _groupTrainingTypeName;

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
