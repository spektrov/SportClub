using System.Data.Entity.ModelConfiguration;
using SportClub.Model;


namespace SportClub.SportClubDbContext
{
    class TrainingInGroupConfig : EntityTypeConfiguration<TrainingInGroup>
    {
        public TrainingInGroupConfig()
        {
            HasKey(training => training.TrainingInGroupId);
            HasRequired(training => training.Client).WithMany(client => client.TrainingInGroups).HasForeignKey(t => t.ClientId);
            HasRequired(training => training.GroupTraining).WithMany(group => group.TrainingInGroups).HasForeignKey(t => t.GroupTrainingId);

            ToTable("TrainingInGroup");
        }
    }
}
