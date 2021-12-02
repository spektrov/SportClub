using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class ClientConfig : EntityTypeConfiguration<Client>
    {
        public ClientConfig()
        {
            HasKey(client => client.ClientId);
            Property(client => client.LastName).IsRequired().HasMaxLength(50);
            Property(client => client.FirstName).IsRequired().HasMaxLength(50);
            Property(client => client.BirthDate).HasColumnType("datetime2").IsOptional();
            Property(client => client.Gender).IsRequired();
            Property(client => client.PhoneNumber).HasMaxLength(15).IsOptional();
            Property(client => client.Email).HasMaxLength(30).IsOptional();
            Property(client => client.RegistrationDate).IsRequired();

            ToTable("Clients");
        }
    }
}
