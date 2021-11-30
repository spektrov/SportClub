namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Gender1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "Gender", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "Gender", c => c.Int(nullable: false));
        }
    }
}
