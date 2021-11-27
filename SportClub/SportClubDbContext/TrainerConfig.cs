using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class TrainerConfig : EntityTypeConfiguration<Trainer>
    {
        public TrainerConfig()
        {
            HasKey(trainer => trainer.TrainerId);
            Property(trainer => trainer.LastName).IsRequired().HasMaxLength(50);
            Property(trainer => trainer.FirstName).IsRequired().HasMaxLength(50);
            Property(trainer => trainer.BirthDate).HasColumnType("datetime2").IsOptional();
            Property(trainer => trainer.Gender).IsRequired().HasMaxLength(20);
            Property(trainer => trainer.PhoneNumber).HasMaxLength(15).IsOptional();
            Property(trainer => trainer.Email).HasMaxLength(30).IsOptional();
            Property(trainer => trainer.ApplyDate).HasColumnType("datetime2").IsRequired();
            Property(trainer => trainer.Education).IsOptional();
            Property(trainer => trainer.SportAchivements).IsOptional();
            Property(trainer => trainer.Experience).IsOptional();
            Property(trainer => trainer.Salary).IsOptional();

            ToTable("Trainers");
        }
    }
}
