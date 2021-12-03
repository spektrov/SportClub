namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PersonalTrainings : DbMigration
    {
        public override void Up()
        {
                        CreateTable(
                "dbo.PersonalTrainings",
                c => new
                    {
                        PersonalTrainingId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        TrainingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.PersonalTrainingId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PersonalTrainings", "ClientId", "dbo.Trainers");
            DropForeignKey("dbo.PersonalTrainings", "TrainerId", "dbo.Clients");
            DropTable("dbo.PersonalTrainings");
        }
    }
}
