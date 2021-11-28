using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using SportClub.SportClubDbContext;
using SportClub.Model;


namespace SportClub.ViewModel
{
    class ClientsTabViewModel : ViewModelBase
    {
        private IList<Client> _filteredClientList;

        private static readonly List<string> _genderTypes = new List<string> {
            "Мужской",
            "Женский",
            "Другой",
        };

        public SportClubContext Context { get; }
        public Client ClientInfo { get; set; } = new Client();
        public Client ClientFilter { get; set; } = new Client();
        public Client SelectedClient { get; set; }

        public List<string> GenderTypes { get => _genderTypes; }



        public IList<Client> FilteredClientList
        {
            get => _filteredClientList;
            set
            {
                _filteredClientList = value;
                RaisePropertyChanged();
            }
        }

        public ClientsTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Clients.Load();
        }

        private RelayCommand _addClientCommand;
        private RelayCommand _updateClientCommand;
        private RelayCommand _deleteClientCommand;
        private RelayCommand<object> _resetFilterClientCommand;
        private RelayCommand _clientsGridSelectionChangedCommand;
        private RelayCommand _clientsFilterChangedCommand;

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
                    if (parameters is Tuple<TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox> tuple)
                    {
                        tuple.Item1.Text = string.Empty;
                        tuple.Item2.Text = string.Empty;
                        tuple.Item3.SelectedDate = null;
                        tuple.Item4.SelectedIndex = -1;
                        tuple.Item5.Text = string.Empty;
                        tuple.Item6.Text = string.Empty;
                    }
                },
                parameters =>
                {
                    if (parameters == null) return false;
                    if (parameters is Tuple<TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox> tuple)
                    {
                        if (string.IsNullOrEmpty(tuple.Item1.Text)
                            && string.IsNullOrEmpty(tuple.Item2.Text)
                            && tuple.Item3.SelectedDate == null
                            && tuple.Item4.SelectedIndex == -1
                            && string.IsNullOrEmpty(tuple.Item5.Text)
                            && string.IsNullOrEmpty(tuple.Item6.Text)
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
                    if (!string.IsNullOrEmpty(ClientFilter.FirstName))
                    {
                        queryResult = queryResult.Where(client => client.FirstName.Contains(ClientFilter.FirstName));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.LastName))
                    {
                        queryResult = queryResult.Where(client => client.LastName.Contains(ClientFilter.LastName));
                    }
                    if (ClientFilter.BirthDate != null)
                    {
                        queryResult = queryResult.Where(client => client.BirthDate != null && client.BirthDate == (ClientFilter.BirthDate));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.Gender) && ClientFilter.Gender != "-1")
                    {
                        var gender = ""; 
                        if (ClientFilter.Gender == "0") gender = "Мужской";
                        else if (ClientFilter.Gender == "1") gender = "Женский";
                        else if (ClientFilter.Gender == "2") gender = "Другой";
                        queryResult = queryResult.Where(client => client.Gender == gender);
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.PhoneNumber))
                    {
                        queryResult = queryResult.Where(client => !string.IsNullOrEmpty(client.PhoneNumber) && client.PhoneNumber.Contains(ClientFilter.PhoneNumber));
                    }
                    if (!string.IsNullOrEmpty(ClientFilter.Email))
                    {
                        queryResult = queryResult.Where(client => !string.IsNullOrEmpty(client.Email) && client.Email.Contains(ClientFilter.Email));
                    }
                    //if (ClientFilter.RegistrationDate != null)
                    //{
                    //    queryResult = queryResult.Where(client => client.RegistrationDate == ClientFilter.RegistrationDate);
                    //}
                    FilteredClientList = queryResult?.ToList();
                }));


    }
}
