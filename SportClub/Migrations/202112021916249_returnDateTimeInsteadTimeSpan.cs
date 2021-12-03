namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class returnDateTimeInsteadTimeSpan : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WorkShifts", "StartHour", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.WorkShifts", "EndHour", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WorkShifts", "EndHour", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.WorkShifts", "StartHour", c => c.Time(nullable: false, precision: 7));
        }
    }
}
