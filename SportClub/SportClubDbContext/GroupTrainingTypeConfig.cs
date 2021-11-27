using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;


namespace SportClub.SportClubDbContext
{
    class GroupTrainingTypeConfig : EntityTypeConfiguration<GroupTrainingType>
    {
        public GroupTrainingTypeConfig()
        {
            HasKey(type => type.GroupTrainingTypeId);
            Property(type => type.GroupTrainingTypeName).IsRequired().HasMaxLength(50);

            ToTable("GroupTrainingTypes");
        }
    }
}
