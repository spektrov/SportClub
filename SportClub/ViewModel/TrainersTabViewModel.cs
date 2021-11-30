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
    class TrainersTabViewModel : ViewModelBase
    {
        private IList<Trainer> _filteredTrainerList;

        private static readonly List<string> _genderTypes = new List<string> {
            "Мужской",
            "Женский",
            "Другой",
        };

        public SportClubContext Context { get; }
        public Trainer TrainerInfo { get; set; } = new Trainer() { ApplyDate = DateTime.Now };
        public Trainer TrainerFilter { get; set; } = new Trainer();
        public Trainer SelectedTrainer { get; set; }

        public List<string> GenderTypes { get => _genderTypes; }

        public int? LessExperience { get; set; }
        public int? MoreExperience { get; set; }
        public decimal? LessSalary { get; set; }
        public decimal? MoreSalary { get; set; }

        public IList<Trainer> FilteredTrainerList
        {
            get => _filteredTrainerList;
            set
            {
                _filteredTrainerList = value;
                RaisePropertyChanged();
            }
        }

        public TrainersTabViewModel(SportClubContext context)
        {
            Context = context;
            Context.Trainers.Load();
            
        }

        private RelayCommand _addTrainerCommand;
        private RelayCommand _updateTrainerCommand;
        private RelayCommand _deleteTrainerCommand;
        private RelayCommand<object> _resetFilterTrainerCommand;
        private RelayCommand _trainersGridSelectionChangedCommand;
        private RelayCommand _trainersFilterChangedCommand;
        private RelayCommand _saveDocumentTrainer;


        public ICommand AddTrainerCommand =>
            _addTrainerCommand ??
            (_addTrainerCommand = new RelayCommand(
                () =>
                {
                    Context.Trainers.Add(new Trainer
                    {
                        FirstName = TrainerInfo.FirstName,
                        LastName = TrainerInfo.LastName,
                        BirthDate = TrainerInfo.BirthDate,
                        Gender = TrainerInfo.Gender,
                        PhoneNumber = TrainerInfo.PhoneNumber,
                        Email = TrainerInfo.Email,
                        ApplyDate = TrainerInfo.ApplyDate,
                        SportAchivements = TrainerInfo.SportAchivements,
                        Education = TrainerInfo.Education,
                        Experience = TrainerInfo.Experience,
                        Salary = TrainerInfo.Salary
                    });
                    Context.SaveChanges();
                },
                () =>
                {
                    if (string.IsNullOrEmpty(TrainerInfo.FirstName)
                        || string.IsNullOrEmpty(TrainerInfo.LastName)
                        || Equals(TrainerInfo.Gender, null)
                        || Equals(TrainerInfo.ApplyDate, null)
                        || TrainerInfo.FirstName.Length < 2
                        || TrainerInfo.LastName.Length < 2
                    )
                    {
                        return false;
                    }
                    return true;
                }
                ));

        public ICommand UpdateTrainerCommand =>
            _updateTrainerCommand ??
            (_updateTrainerCommand = new RelayCommand(
                () =>
                {
                    SelectedTrainer.FirstName = TrainerInfo.FirstName;
                    SelectedTrainer.LastName = TrainerInfo.LastName;
                    SelectedTrainer.BirthDate = TrainerInfo.BirthDate;
                    SelectedTrainer.Gender = TrainerInfo.Gender;
                    SelectedTrainer.PhoneNumber = TrainerInfo.PhoneNumber;
                    SelectedTrainer.Email = TrainerInfo.Email;
                    SelectedTrainer.ApplyDate = TrainerInfo.ApplyDate;
                    SelectedTrainer.SportAchivements = TrainerInfo.SportAchivements;
                    SelectedTrainer.Education = TrainerInfo.Education;
                    SelectedTrainer.Experience = TrainerInfo.Experience;
                    SelectedTrainer.Salary = TrainerInfo.Salary;
                },
                () =>
                {
                    if (SelectedTrainer == null) return false;
                    if (string.IsNullOrEmpty(TrainerInfo.FirstName)
                       || string.IsNullOrEmpty(TrainerInfo.LastName)
                       || Equals(TrainerInfo.Gender, null)
                       || Equals(TrainerInfo.ApplyDate, null)
                       || TrainerInfo.FirstName.Length < 2
                       || TrainerInfo.LastName.Length < 2
                   )
                    {
                        return false;
                    }
                    return true;
                }
                ));

        public ICommand DeleteTrainerCommand =>
            _deleteTrainerCommand ??
            (_deleteTrainerCommand = new RelayCommand(
                () =>
                {
                    Context.Trainers.Remove(SelectedTrainer);
                    Context.SaveChanges();
                },
                () => SelectedTrainer != null
                ));

        public ICommand ResetFilterTrainerCommand =>
            _resetFilterTrainerCommand ??
            (_resetFilterTrainerCommand = new RelayCommand<object>(
                parameters =>
                {
                    if (parameters is Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox,
                        Tuple<TextBox, TextBox, TextBox, TextBox>> tuple)
                    {
                        tuple.Item1.Text = "-1";
                        tuple.Item2.Text = string.Empty;
                        tuple.Item3.Text = string.Empty;
                        tuple.Item4.SelectedDate = null;
                        tuple.Item5.SelectedIndex = -1;
                        tuple.Item6.Text = string.Empty;
                        tuple.Item7.Text = string.Empty;
                        tuple.Rest.Item1.Text = "0";
                        tuple.Rest.Item2.Text = "0";
                        tuple.Rest.Item3.Text = "0";
                        tuple.Rest.Item4.Text = "0";
                    }
                },
                parameters =>
                {
                    if (parameters == null) return false;
                    if (parameters is Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox,
                        Tuple<TextBox, TextBox, TextBox, TextBox>> tuple)
                    {
                        if (string.IsNullOrEmpty(tuple.Item1.Text)
                            && string.IsNullOrEmpty(tuple.Item2.Text)
                            && string.IsNullOrEmpty(tuple.Item3.Text)
                            && tuple.Item4.SelectedDate == null
                            && tuple.Item5.SelectedIndex == -1
                            && string.IsNullOrEmpty(tuple.Item6.Text)
                            && string.IsNullOrEmpty(tuple.Item7.Text)
                            && string.IsNullOrEmpty(tuple.Rest.Item1.Text)
                            && string.IsNullOrEmpty(tuple.Rest.Item2.Text)
                            && string.IsNullOrEmpty(tuple.Rest.Item3.Text)
                            && string.IsNullOrEmpty(tuple.Rest.Item4.Text)
                            )

                            return false;
                        return true;
                    }
                    return false;
                }
                ));

        public ICommand TrainersGridSelectionChangedCommand =>
            _trainersGridSelectionChangedCommand ??
            (_trainersGridSelectionChangedCommand = new RelayCommand(
                () =>
                {
                    TrainerInfo.FirstName = SelectedTrainer.FirstName;
                    TrainerInfo.LastName = SelectedTrainer.LastName;
                    TrainerInfo.BirthDate = SelectedTrainer.BirthDate;
                    TrainerInfo.Gender = SelectedTrainer.Gender;
                    TrainerInfo.PhoneNumber = SelectedTrainer.PhoneNumber;
                    TrainerInfo.Email = SelectedTrainer.Email;
                    TrainerInfo.ApplyDate = SelectedTrainer.ApplyDate;
                    TrainerInfo.SportAchivements = SelectedTrainer.SportAchivements;
                    TrainerInfo.Education = SelectedTrainer.Education;
                    TrainerInfo.Experience = SelectedTrainer.Experience;
                    TrainerInfo.Salary = SelectedTrainer.Salary;
                },
                () => SelectedTrainer != null));

        public ICommand TrainersFilterChangedCommand =>
            _trainersFilterChangedCommand ??
            (_trainersFilterChangedCommand = new RelayCommand(
                () =>
                {
                    IEnumerable<Trainer> queryResult = Context.Trainers.Local;

                    if (TrainerFilter.TrainerId != -1)
                    {
                        queryResult = queryResult.Where(trainer => trainer.TrainerId.ToString().
                            Contains(TrainerFilter.TrainerId.ToString()));
                    }
                    if (!string.IsNullOrEmpty(TrainerFilter.FirstName))
                    {
                        queryResult = queryResult.Where(trainer => trainer.FirstName.ToLower().Contains(TrainerFilter.FirstName.ToLower()));
                    }
                    if (!string.IsNullOrEmpty(TrainerFilter.LastName))
                    {
                        queryResult = queryResult.Where(trainer => trainer.LastName.ToLower().Contains(TrainerFilter.LastName.ToLower()));
                    }
                    if (TrainerFilter.BirthDate != null)
                    {
                        queryResult = queryResult.Where(trainer => trainer.BirthDate != null && trainer.BirthDate == TrainerFilter.BirthDate);
                    }
                    if (!string.IsNullOrEmpty(TrainerFilter.Gender) && TrainerFilter.Gender != "-1")
                    {
                        queryResult = queryResult.Where(trainer => trainer.Gender.Equals(TrainerFilter.Gender));
                    }
                    if (!string.IsNullOrEmpty(TrainerFilter.PhoneNumber))
                    {
                        queryResult = queryResult.Where(trainer => !string.IsNullOrEmpty(trainer.PhoneNumber) && trainer.PhoneNumber.Contains(TrainerFilter.PhoneNumber));
                    }
                    if (!string.IsNullOrEmpty(TrainerFilter.Email))
                    {
                        queryResult = queryResult.Where(trainer => !string.IsNullOrEmpty(trainer.Email) && trainer.Email.Contains(TrainerFilter.Email));
                    }
                    if (LessExperience > 0)
                    {
                        queryResult = queryResult.Where(trainer => trainer.Experience < LessExperience);
                    }
                    if (MoreExperience > 0)
                    {
                        queryResult = queryResult.Where(trainer => trainer.Experience >= MoreExperience);
                    }
                    if (LessSalary > 0)
                    {
                        queryResult = queryResult.Where(trainer => trainer.Salary < LessSalary);
                    }
                    if (MoreSalary > 0)
                    {
                        queryResult = queryResult.Where(trainer => trainer.Salary >= MoreSalary);
                    }

                    FilteredTrainerList = queryResult?.ToList();
                }
                ));

        public ICommand SaveDocumentTrainer =>
            _saveDocumentTrainer ??
            (_saveDocumentTrainer = new RelayCommand(
                () =>
                {
                    var saveDialog = new SaveFileDialog
                    {
                        DefaultExt = ".docx",
                        Filter = "Document (.doc)|*.docx",
                        FileName = SelectedTrainer.FirstName + SelectedTrainer.LastName + "Тренер"
                    };
                    var result = saveDialog.ShowDialog();
                    if (result == true)
                    {
                        var reportCreator = new TrainerDocument();

                        var report = reportCreator.GenerateTrainerDoc(
                            SelectedTrainer.TrainerId, SelectedTrainer.LastName, SelectedTrainer.FirstName, SelectedTrainer.ApplyDate, SelectedTrainer.Salary);
                        report.Save(saveDialog.FileName);
                    }
                },
                () => Context.Trainers.Count() != 0 && SelectedTrainer != null));
    }

    public static class DtHelper1
    {
        public static DateTime TrainerStartDate
        {
            get { return Convert.ToDateTime("01 September 2021"); }
        }
    }
}
