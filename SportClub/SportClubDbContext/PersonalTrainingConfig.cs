using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class PersonalTrainingConfig : EntityTypeConfiguration<PersonalTraining>
    {
       public PersonalTrainingConfig()
        {
            HasKey(training => training.PersonalTrainingId);
            HasRequired(training => training.Client).WithMany(client => client.PersonalTrainings).HasForeignKey(t => t.ClientId).WillCascadeOnDelete(true);
            HasRequired(training => training.Client).WithMany(trainer => trainer.PersonalTrainings).HasForeignKey(t => t.TrainerId).WillCascadeOnDelete(true);
            Property(training => training.TrainingDate).HasColumnType("datetime2").IsRequired();
            Property(training => training.StartTime).HasColumnType("datetime2").IsRequired();

            ToTable("PersonalTrainings");
        }
    }
}
