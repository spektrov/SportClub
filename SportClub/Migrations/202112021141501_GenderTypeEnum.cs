namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenderTypeEnum : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Trainers", "Gender", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainers", "Gender", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Clients", "Gender", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
