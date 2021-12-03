using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;


namespace SportClub.ViewModel
{
    class RoomsTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public Room RoomInfo { get; set; } = new Room();
        public Room SelectedRoom { get; set; }

        public RoomsTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Rooms.Load();
        }

        private RelayCommand _addRoomCommand;
        private RelayCommand _updateRoomCommand;
        private RelayCommand _deleteRoomCommand;
        private RelayCommand _roomGridSelectionChangedCommand;

        public ICommand AddRoomCommand =>
            _addRoomCommand ??
            (_addRoomCommand = new RelayCommand(
                () =>
                {
                    Context.Rooms.Add(new Room
                    {
                        RoomName = RoomInfo.RoomName
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(RoomInfo.RoomName))
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateRoomCommand =>
            _updateRoomCommand ??
            (_updateRoomCommand = new RelayCommand(
                () =>
                {
                    SelectedRoom.RoomName = RoomInfo.RoomName;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedRoom == null) return false;
                    if (string.IsNullOrEmpty(RoomInfo.RoomName))
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteRoomCommand =>
           _deleteRoomCommand ??
           (_deleteRoomCommand = new RelayCommand(
               () =>
               {
                   Context.Rooms.Remove(SelectedRoom);
                   Context.SaveChanges();
               },
               () => SelectedRoom != null));

        public ICommand RoomGridSelectionChangedCommand =>
           _roomGridSelectionChangedCommand ??
           (_roomGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   RoomInfo.RoomName = SelectedRoom.RoomName;
               },
               () => SelectedRoom != null));
    }
}
