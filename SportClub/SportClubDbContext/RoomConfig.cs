using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class RoomConfig : EntityTypeConfiguration<Room>
    {
        public RoomConfig()
        {
            HasKey(room => room.RoomId);
            Property(room => room.RoomName).IsRequired().HasMaxLength(50);
        }
    }
}
