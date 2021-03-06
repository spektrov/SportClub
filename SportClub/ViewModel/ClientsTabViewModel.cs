using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using Microsoft.Win32;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;
using SportClub.Miscellaneous;


namespace SportClub.ViewModel
{
    class ClientsTabViewModel : ViewModelBase
    {

        private IList<Client> _filteredClientList;
        private IList<Training> _trainingsList;

        public SportClubContext Context { get; }
        public Client ClientInfo { get; set; } = new Client() { RegistrationDate = DateTime.Now };
        public Client ClientFilter { get; set; } = new Client() { Gender = Model.Genders.Не_Выбрано};
        public Client SelectedClient { get; set; }

        public Array Genders { get => Enum.GetValues(typeof(Genders)); }

        public IList<Client> FilteredClientList
        {
            get => _filteredClientList;
            set
            {
                _filteredClientList = value;
                RaisePropertyChanged();
            }
        }

        public IList<Training> TrainingsList
        {
            get => _trainingsList;
            set
            {
                _trainingsList = value;
                RaisePropertyChanged();
            }
        }

        public ClientsTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Clients.Load();
            Context.Trainings.Load();
        }


        private RelayCommand _addClientCommand;
        private RelayCommand _updateClientCommand;
        private RelayCommand _deleteClientCommand;
        private RelayCommand<object> _resetFilterClientCommand;
        private RelayCommand _clientsGridSelectionChangedCommand;
        private RelayCommand _clientsFilterChangedCommand;
        private RelayCommand _trainingsSelectCommand;
        private RelayCommand _saveDocumentClient;

        public ICommand AddClientCommand =>
            _addClientCommand ??
            (_addClientCommand = new RelayCommand(
                () =>
                {
                    Context.Clients.Add(new Client
                    {
                        FirstName = ClientInfo.FirstName,
                        LastName = ClientInfo.LastName,
                        BirthDate = ClientInfo.BirthDate,
                        Gender = ClientInfo.Gender,
                        PhoneNumber = ClientInfo.PhoneNumber,
                        Email = ClientInfo.Email,
                        RegistrationDate = ClientInfo.RegistrationDate,
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(ClientInfo.FirstName)
                        || ClientInfo.FirstName.Length < 2
                        || string.IsNullOrEmpty(ClientInfo.LastName)
                        || ClientInfo.LastName.Length < 2
                        || Equals(ClientInfo.Gender, null)
                        || Equals(ClientInfo.RegistrationDate, null)
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand UpdateClientCommand =>
            _updateClientCommand ??
            (_updateClientCommand = new RelayCommand(
                () =>
                {
                    SelectedClient.FirstName = ClientInfo.FirstName;
                    SelectedClient.LastName = ClientInfo.LastName;
                    SelectedClient.BirthDate = ClientInfo.BirthDate;
                    SelectedClient.Gender = ClientInfo.Gender;
                    SelectedClient.PhoneNumber = ClientInfo.PhoneNumber;
                    SelectedClient.Email = ClientInfo.Email;
                    SelectedClient.RegistrationDate = ClientInfo.RegistrationDate;

                    Context.SaveChanges();
                },
                () =>
                {
                    if (SelectedClient == null) return false;
                    if (string.IsNullOrEmpty(ClientInfo.FirstName)
                        || ClientInfo.FirstName.Length < 2
                        || string.IsNullOrEmpty(ClientInfo.LastName)
                        || ClientInfo.LastName.Length < 2
                        || Equals(ClientInfo.Gender, null)
                        || Equals(ClientInfo.RegistrationDate, null)
                        )
                    {
                        return false;
                    }
                    return true;
                }));

        public ICommand DeleteClientCommand =>
           _deleteClientCommand ??
           (_deleteClientCommand = new RelayCommand(
               () =>
               {
                   Context.Clients.Remove(SelectedClient);
                   Context.SaveChanges();
               },
               () => SelectedClient != null
               ));

        public RelayCommand<object> ResetFilterClientCommand =>
            _resetFilterClientCommand ??
            (_resetFilterClientCommand = new RelayCommand<object>(
                parameters =>
                {
                    if (parameters is Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox> tuple)
                    {
                        tuple.Item1.Text = "-1";
                        tuple.Item2.Text = string.Empty;
                        tuple.Item3.Text = string.Empty;
                        tuple.Item4.SelectedDate = null;
                        tuple.Item5.SelectedIndex = 0;
                        tuple.Item6.Text = string.Empty;
                        tuple.Item7.Text = string.Empty;
                    }
                },
                parameters =>
                {
                    if (parameters == null) return false;
                    if (parameters is Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox> tuple)
                    {
                        if (string.IsNullOrEmpty(tuple.Item1.Text)
                            && string.IsNullOrEmpty(tuple.Item2.Text)
                            && string.IsNullOrEmpty(tuple.Item3.Text)
                            && tuple.Item4.SelectedDate == null
                            && tuple.Item5.SelectedIndex == -1
                            && string.IsNullOrEmpty(tuple.Item6.Text)
                            && string.IsNullOrEmpty(tuple.Item7.Text)
                            )
                            return false;
                        return true;
                    }
                    return false;
                }));

        public ICommand ClientsGridSelectionChangedCommand =>
            _clientsGridSelectionChangedCommand ??
            (_clientsGridSelectionChangedCommand =
                new RelayCommand(
                    () =>
                    { 
                        ClientInfo.FirstName = SelectedClient.FirstName;
                        ClientInfo.LastName = SelectedClient.LastName;
                        ClientInfo.BirthDate = SelectedClient.BirthDate;
                        ClientInfo.Gender = SelectedClient.Gender;
                        ClientInfo.PhoneNumber = SelectedClient.PhoneNumber;
                        ClientInfo.Email = SelectedClient.Email;
                        ClientInfo.RegistrationDate = SelectedClient.RegistrationDate;
                    },
                    () => SelectedClient != null));

        public ICommand ClientsFilterChangedCommand =>
            _clientsFilterChangedCommand ?? (_clientsFilterChangedCommand =
                new RelayCommand(() =>
                {
                    IEnumerable<Client> queryResult = Context.Clients.Local;
                    if (ClientFilter.ClientId != -1)
                    {
                        queryResult = queryResult.Where(client => client.ClientId.ToString().
                            Contains(ClientFilter.ClientId.ToString()));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.FirstName))
                    {
                        queryResult = queryResult.Where(client => client.FirstName.ToLower().Contains(ClientFilter.FirstName.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.LastName))
                    {
                        queryResult = queryResult.Where(client => client.LastName.ToLower().Contains(ClientFilter.LastName.ToLower()));
                    }
                    if (ClientFilter.BirthDate != null)
                    {
                        queryResult = queryResult.Where(client => client.BirthDate != null && client.BirthDate == ClientFilter.BirthDate);
                    }
                    if (ClientFilter.Gender != 0)
                    {
                        queryResult = queryResult.Where(client => client.Gender.Equals(ClientFilter.Gender));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.PhoneNumber))
                    {
                        queryResult = queryResult.Where(client => !string.IsNullOrEmpty(client.PhoneNumber) && client.PhoneNumber.Contains(ClientFilter.PhoneNumber));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.Email))
                    {
                        queryResult = queryResult.Where(client => !string.IsNullOrEmpty(client.Email) && client.Email.Contains(ClientFilter.Email));
                    }

                    FilteredClientList = queryResult?.ToList();
                }));

        public ICommand TrainingSelectChangedCommand =>
            _trainingsSelectCommand ?? (_trainingsSelectCommand =
            new RelayCommand(() =>
           {
               IEnumerable<Training> trainings = Context.Trainings.Local;
               trainings = trainings.Where(training => training.ClientId.Equals(SelectedClient?.ClientId));
               TrainingsList = trainings?.ToList();
           },
                () => SelectedClient != null));

        public ICommand SaveDocumentClient =>
            _saveDocumentClient ?? (_saveDocumentClient =
            new RelayCommand(() =>
            {
                var saveDialog = new SaveFileDialog
                {
                    DefaultExt = ".docx",
                    Filter = "Document (.doc)|*.docx",
                    FileName = SelectedClient.FirstName + SelectedClient.LastName
                };
                var result = saveDialog.ShowDialog();
                if (result == true)
                {
                    var reportCreator = new ClientDocument();
                    var report = reportCreator.GenerateClientDoc(SelectedClient.ClientId, SelectedClient.LastName, SelectedClient.FirstName, SelectedClient.RegistrationDate);
                    report.Save(saveDialog.FileName);
                }
            },
            () => Context.Clients.Count() != 0 && SelectedClient != null));
    }

    public static class DtHelper
    {
        public static DateTime ClientStartDate
        {
            get { return Convert.ToDateTime("01 October 2021"); }
        }
    } 
}
