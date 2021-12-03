using System.Data.Entity;
using System.Windows;
using SportClub.SportClubDbContext;
using SportClub.ViewModel;
using SportClub.Miscellaneous;

namespace SportClub.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SportClubContext Context { get; }

        public MainWindow()
        {
            InitializeComponent();

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<SportClubContext>());
           
            Context = new SportClubContext();

            var notificationEmail = new SubscriptionNotificationEmail(Context);
            notificationEmail.SendAllNotifications(3);

          
            ClientsTab.DataContext = new ClientsTabViewModel(Context);
            TrainersTab.DataContext = new TrainersTabViewModel(Context);
            TariffTab.DataContext = new TariffTabViewModel(Context);
            TrainingTab.DataContext = new TrainingTabViewModel(Context);
            SubscriptionTab.DataContext = new SubscriptionsTabViewModel(Context);
            GroupTrainingTypeTab.DataContext = new GroupTrainingTypeTabViewModel(Context);
            RoomsTab.DataContext = new RoomsTabViewModel(Context);
            WorkShiftTab.DataContext = new WorkShiftTabViewModel(Context);
            ScheduleTab.DataContext = new ScheduleTabViewModel(Context);
            PersonalTrainingTab.DataContext = new PersonalTrainingTabViewModel(Context);
            GroupTrainingsTab.DataContext = new GroupTrainingTabViewModel(Context);
            TrainingInGroupTab.DataContext = new TrainingInGroupTabViewModel(Context);
        }

        private void SQLquery_Click(object sender, RoutedEventArgs e)
        {
            var window = new QueryEdit();
            window.DataContext = new TopMenuViewModel();
            window.Show();
        }
    }
}
