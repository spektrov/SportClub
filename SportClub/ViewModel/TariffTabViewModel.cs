using System.Data.Entity;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;

namespace SportClub.ViewModel
{
    class TariffTabViewModel : ViewModelBase
    {
        public SportClubContext Context { get; }
        public Tariff TariffInfo { get; set; } = new Tariff();
        public Tariff SelectedTariff { get; set; }

        public TariffTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Tariffs.Load();
        }

        private RelayCommand _addTariffCommand;
        private RelayCommand _updateTariffCommand;
        private RelayCommand _deleteTafiffCommand;
        private RelayCommand _tariffsGridSelectionChangedCommand;

        public ICommand AddTariffCommand =>
            _addTariffCommand ??
            (_addTariffCommand = new RelayCommand(
                () =>
                {
                    Context.Tariffs.Add(new Tariff
                    {
                        Price = TariffInfo.Price,
                        VisitNumber = TariffInfo.VisitNumber,
                        PersonalTraining = TariffInfo.PersonalTraining,
                        GroupTraining = TariffInfo.GroupTraining,
                        Duration = TariffInfo.Duration,
                        AdditionalInformation = TariffInfo.AdditionalInformation
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (TariffInfo.Price <= 0
                        || TariffInfo.VisitNumber <= 0
                        || TariffInfo.PersonalTraining < 0
                        || TariffInfo.GroupTraining < 0
                        || TariffInfo.Duration <= 0
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateTariffCommand =>
           _updateTariffCommand ??
           (_updateTariffCommand = new RelayCommand(
               () =>
               {
                   SelectedTariff.Price = TariffInfo.Price;
                   SelectedTariff.VisitNumber = TariffInfo.VisitNumber;
                   SelectedTariff.PersonalTraining = TariffInfo.PersonalTraining;
                   SelectedTariff.GroupTraining = TariffInfo.GroupTraining;
                   SelectedTariff.Duration = TariffInfo.Duration;
                   SelectedTariff.AdditionalInformation = TariffInfo.AdditionalInformation;
               },
               () =>
               {
                   if (SelectedTariff == null) return false;
                   if (TariffInfo.Price <= 0
                        || TariffInfo.VisitNumber <= 0
                        || TariffInfo.PersonalTraining < 0
                        || TariffInfo.GroupTraining < 0
                        || TariffInfo.Duration <= 0
                        )
                   {
                       return false;
                   }
                   return true;
               }));

        public ICommand DeleteTariffCommand =>
           _deleteTafiffCommand ??
           (_deleteTafiffCommand = new RelayCommand(
               () =>
               {
                   Context.Tariffs.Remove(SelectedTariff);
                   Context.SaveChanges();
               },
               () => SelectedTariff != null));

        public ICommand TariffsGridSelectionChangedCommand =>
           _tariffsGridSelectionChangedCommand ??
           (_tariffsGridSelectionChangedCommand = new RelayCommand(
               () =>
               {
                   TariffInfo.Price = SelectedTariff.Price;
                   TariffInfo.VisitNumber = SelectedTariff.VisitNumber;
                   TariffInfo.PersonalTraining = SelectedTariff.PersonalTraining;
                   TariffInfo.GroupTraining = SelectedTariff.GroupTraining;
                   TariffInfo.Duration = SelectedTariff.Duration;
                   TariffInfo.AdditionalInformation = SelectedTariff.AdditionalInformation;
               },
               () => SelectedTariff != null ));
    }
}
