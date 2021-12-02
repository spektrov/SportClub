using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;


namespace SportClub.ViewModel
{
    class GroupTrainingTypeTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public GroupTrainingType GroupTrainingTypeInfo { get; set; } = new GroupTrainingType();
        public GroupTrainingType SelectedGroupTrainingType { get; set; }

        public GroupTrainingTypeTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.GroupTrainingTypes.Load();
        }

        private RelayCommand _addGroupTrainingTypeCommand;
        private RelayCommand _updateGroupTrainingTypeCommand;
        private RelayCommand _deleteGroupTrainingTypeCommand;
        private RelayCommand _groupTrainingTypeGridSelectionChangedCommand;

        public ICommand AddGroupTrainingTypeCommand =>
            _addGroupTrainingTypeCommand ??
            (_addGroupTrainingTypeCommand = new RelayCommand(
                () =>
                {
                    Context.GroupTrainingTypes.Add(new GroupTrainingType
                    {
                        GroupTrainingTypeName = GroupTrainingTypeInfo.GroupTrainingTypeName
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(GroupTrainingTypeInfo.GroupTrainingTypeName))
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateGroupTrainingTypeCommand =>
            _updateGroupTrainingTypeCommand ??
            (_updateGroupTrainingTypeCommand = new RelayCommand(
                () =>
                {
                    SelectedGroupTrainingType.GroupTrainingTypeName = GroupTrainingTypeInfo.GroupTrainingTypeName;
                },
                () =>
                {
                    if (SelectedGroupTrainingType == null) return false;
                    if (string.IsNullOrEmpty(GroupTrainingTypeInfo.GroupTrainingTypeName))
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteGroupTrainingTypeCommand =>
           _deleteGroupTrainingTypeCommand ??
           (_deleteGroupTrainingTypeCommand = new RelayCommand(
               () =>
               {
                   Context.GroupTrainingTypes.Remove(SelectedGroupTrainingType);
                   Context.SaveChanges();
               },
               () => SelectedGroupTrainingType != null));

        public ICommand GroupTrainingTypeGridSelectionChangedCommand =>
           _groupTrainingTypeGridSelectionChangedCommand ??
           (_groupTrainingTypeGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   GroupTrainingTypeInfo.GroupTrainingTypeName = SelectedGroupTrainingType.GroupTrainingTypeName;
               },
               () => SelectedGroupTrainingType != null));
    }
}
