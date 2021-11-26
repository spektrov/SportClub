using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using SportClub.Model;


namespace SportClub.SportClubDbContext
{
    class ScheduleConfig : EntityTypeConfiguration<Schedule>
    {
        public ScheduleConfig()
        {
            HasKey(schedule => new { schedule.TrainerId, schedule.WorkShiftId });
            HasRequired(schedule => schedule.Trainer).WithMany(trainer => trainer.Schedules).HasForeignKey(s => s.TrainerId);
            HasRequired(schedule => schedule.WorkShift).WithMany(shift => shift.Schedules).HasForeignKey(s => s.WorkShiftId);
        }
    }
}
