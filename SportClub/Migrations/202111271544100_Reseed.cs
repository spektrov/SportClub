namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reseed : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT('Clients', RESEED, 100000)");
            Sql("DBCC CHECKIDENT('Trainers', RESEED, 1000)");
        }
        
        public override void Down()
        {
        }
    }
}
