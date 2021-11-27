using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;


namespace SportClub.SportClubDbContext
{
    class TrainingInGroupConfig : EntityTypeConfiguration<TrainingInGroup>
    {
        public TrainingInGroupConfig()
        {
            HasKey(training => new { training.ClientId, training.GroupTrainingId });
            HasRequired(training => training.Client).WithMany(client => client.TrainingInGroups).HasForeignKey(t => t.ClientId);
            HasRequired(training => training.GroupTraining).WithMany(group => group.TrainingInGroups).HasForeignKey(t => t.GroupTrainingId);

            ToTable("TrainingInGroup");
        }
    }
}
