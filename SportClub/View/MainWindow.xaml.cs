using System;
using System.Data.Entity;
using System.Windows;
using SportClub.SportClubDbContext;
using SportClub.Model;
using SportClub.ViewModel;

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
          
            ClientsTab.DataContext = new ClientsTabViewModel(Context);
        }
    }
}
