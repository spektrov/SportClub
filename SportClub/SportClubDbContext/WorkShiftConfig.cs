using System.Data.Entity.ModelConfiguration;
using SportClub.Model;

namespace SportClub.SportClubDbContext
{
    class WorkShiftConfig : EntityTypeConfiguration<WorkShift>
    {
        public WorkShiftConfig()
        {
            HasKey(shift => shift.WorkShiftId);
            Property(shift => shift.DayOfWeek).IsRequired();
            Property(shift => shift.StartHour).IsRequired();
            Property(shift => shift.EndHour).IsRequired();

            ToTable("WorkShifts");
        }
    }
}
