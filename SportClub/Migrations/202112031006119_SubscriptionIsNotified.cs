namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubscriptionIsNotified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subscriptions", "IsNotified", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subscriptions", "IsNotified");
        }
    }
}
