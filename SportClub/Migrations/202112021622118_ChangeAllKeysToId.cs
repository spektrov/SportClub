namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAllKeysToId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TrainingInGroup");
            DropPrimaryKey("dbo.Schedule");
            AddColumn("dbo.TrainingInGroup", "TrainingInGroupId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Schedule", "ScheduleId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TrainingInGroup", "TrainingInGroupId");
            AddPrimaryKey("dbo.Schedule", "ScheduleId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Schedule");
            DropPrimaryKey("dbo.TrainingInGroup");
            DropColumn("dbo.Schedule", "ScheduleId");
            DropColumn("dbo.TrainingInGroup", "TrainingInGroupId");
            AddPrimaryKey("dbo.Schedule", new[] { "TrainerId", "WorkShiftId" });
            AddPrimaryKey("dbo.TrainingInGroup", new[] { "ClientId", "GroupTrainingId" });
        }
    }
}
