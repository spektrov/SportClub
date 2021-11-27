using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class SportClubContext : DbContext
    {

        public SportClubContext()
            : base("SportClubDbConnectionString")
        {

        }
        

        public DbSet<Client> Clients { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }
        public DbSet<GroupTrainingType> GroupTrainingTypes { get; set; }
        public DbSet<PersonalTraining> PersonalTrainings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<TrainingInGroup> TrainingInGroups { get; set; }
        public DbSet<WorkShift> WorkShifts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ClientConfig());
            modelBuilder.Configurations.Add(new GroupTrainingConfig());
            modelBuilder.Configurations.Add(new GroupTrainingTypeConfig());
            modelBuilder.Configurations.Add(new PersonalTrainingConfig());
            modelBuilder.Configurations.Add(new RoomConfig());
            modelBuilder.Configurations.Add(new ScheduleConfig());
            modelBuilder.Configurations.Add(new SubscriptionConfig());
            modelBuilder.Configurations.Add(new TariffConfig());
            modelBuilder.Configurations.Add(new TrainerConfig());
            modelBuilder.Configurations.Add(new TrainingConfig());
            modelBuilder.Configurations.Add(new TrainingInGroupConfig());
            modelBuilder.Configurations.Add(new WorkShiftConfig());
        }
    }
}
