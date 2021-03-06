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
    class Room : INotifyPropertyChanged
    {
        private int _roomId;
        private string _roomName;

        private IList<GroupTraining> _groupTrainings;

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

        public string RoomName
        {
            get => _roomName;
            set
            {
                if (value == _roomName) return;
                _roomName = value;
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

        public Room()
        {
            _groupTrainings = new List<GroupTraining>();
        }

        public override string ToString()
        {
            return RoomName.ToString();
        }
    }
}
