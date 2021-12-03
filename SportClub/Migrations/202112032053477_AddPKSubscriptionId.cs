namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPKSubscriptionId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Subscriptions");
            AddColumn("dbo.Subscriptions", "SubscriptionId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Subscriptions", "SubscriptionId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Subscriptions");
            DropColumn("dbo.Subscriptions", "SubscriptionId");
            AddPrimaryKey("dbo.Subscriptions", new[] { "ClientId", "TariffId" });
        }
    }
}
