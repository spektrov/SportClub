using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class GroupTrainingConfig : EntityTypeConfiguration<GroupTraining>
    {
        public GroupTrainingConfig()
        {
            HasKey(training => training.GroupTrainingId);
            HasRequired(training => training.Trainer).WithMany(trainer => trainer.GroupTrainings).HasForeignKey(t => t.TrainerId);
            HasRequired(training => training.Room).WithMany(trainer => trainer.GroupTrainings).HasForeignKey(t => t.RoomId);
            HasRequired(training => training.GroupTrainingType).WithMany(trainer => trainer.GroupTrainings).HasForeignKey(t => t.GroupTrainingTypeId);
            Property(training => training.Date).HasColumnType("datetime2").IsRequired();
            Property(training => training.StartTime).HasColumnType("datetime2").IsRequired();

            ToTable("GroupTraining");
        }
    }
}
