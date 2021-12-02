namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxAttendersToGroupTrainings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupTraining", "MaxAttenders", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupTraining", "MaxAttenders");
        }
    }
}
