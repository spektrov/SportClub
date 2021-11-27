using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class TrainingConfig : EntityTypeConfiguration<Training>
    {
        public TrainingConfig()
        {
            HasKey(training => training.TrainingId);
            HasRequired(training => training.Client).WithMany(client => client.Trainings).HasForeignKey(t => t.ClientId).WillCascadeOnDelete(true);
            Property(training => training.TrainingDate).HasColumnType("datetime2").IsRequired();

            ToTable("Trainings");
        }
    }
}
