using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class TariffConfig : EntityTypeConfiguration<Tariff>
    {
        public TariffConfig()
        {
            HasKey(tariff => tariff.TariffId);
            Property(tariff => tariff.Price).IsRequired();
            Property(tariff => tariff.VisitNumber).IsRequired();
            Property(tariff => tariff.PersonalTraining).IsRequired();
            Property(tariff => tariff.GroupTraining).IsRequired();
            Property(tariff => tariff.AdditionalInformation).IsOptional();

            ToTable("Tariffs");
        }
    }
}
