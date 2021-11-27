namespace SportClub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(maxLength: 15),
                        Email = c.String(maxLength: 30),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
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
                .ForeignKey("dbo.Clients", t => t.TrainerId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        TrainerId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Gender = c.String(nullable: false, maxLength: 20),
                        PhoneNumber = c.String(maxLength: 15),
                        Email = c.String(maxLength: 30),
                        ApplyDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Education = c.String(),
                        SportAchivements = c.String(),
                        Experience = c.Int(),
                        Salary = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TrainerId);
            
            CreateTable(
                "dbo.GroupTraining",
                c => new
                    {
                        GroupTrainingId = c.Int(nullable: false, identity: true),
                        TrainerId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                        GroupTrainingTypeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.GroupTrainingId)
                .ForeignKey("dbo.GroupTrainingTypes", t => t.GroupTrainingTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.RoomId, cascadeDelete: true)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.RoomId)
                .Index(t => t.GroupTrainingTypeId);
            
            CreateTable(
                "dbo.GroupTrainingTypes",
                c => new
                    {
                        GroupTrainingTypeId = c.Int(nullable: false, identity: true),
                        GroupTrainingTypeName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GroupTrainingTypeId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoomId);
            
            CreateTable(
                "dbo.TrainingInGroup",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        GroupTrainingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClientId, t.GroupTrainingId })
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.GroupTraining", t => t.GroupTrainingId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.GroupTrainingId);
            
            CreateTable(
                "dbo.Schedule",
                c => new
                    {
                        TrainerId = c.Int(nullable: false),
                        WorkShiftId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainerId, t.WorkShiftId })
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .ForeignKey("dbo.WorkShifts", t => t.WorkShiftId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.WorkShiftId);
            
            CreateTable(
                "dbo.WorkShifts",
                c => new
                    {
                        WorkShiftId = c.Int(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                        StartHour = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndHour = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.WorkShiftId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        ClientId = c.Int(nullable: false),
                        TariffId = c.Int(nullable: false),
                        VisitLeft = c.Int(nullable: false),
                        PersonalTrainingLeft = c.Int(nullable: false),
                        GroupTrainingLeft = c.Int(nullable: false),
                        BuyDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ValidityDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => new { t.ClientId, t.TariffId })
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Tariffs", t => t.TariffId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.TariffId);
            
            CreateTable(
                "dbo.Tariffs",
                c => new
                    {
                        TariffId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VisitNumber = c.Int(nullable: false),
                        PersonalTraining = c.Int(nullable: false),
                        GroupTraining = c.Int(nullable: false),
                        Duration = c.Int(nullable: false),
                        AdditionalInformation = c.String(),
                    })
                .PrimaryKey(t => t.TariffId);
            
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingId = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        TrainingDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.TrainingId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainings", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Subscriptions", "TariffId", "dbo.Tariffs");
            DropForeignKey("dbo.Subscriptions", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Schedule", "WorkShiftId", "dbo.WorkShifts");
            DropForeignKey("dbo.Schedule", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.PersonalTrainings", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.TrainingInGroup", "GroupTrainingId", "dbo.GroupTraining");
            DropForeignKey("dbo.TrainingInGroup", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.GroupTraining", "TrainerId", "dbo.Trainers");
            DropForeignKey("dbo.GroupTraining", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.GroupTraining", "GroupTrainingTypeId", "dbo.GroupTrainingTypes");
            DropForeignKey("dbo.PersonalTrainings", "TrainerId", "dbo.Clients");
            DropIndex("dbo.Trainings", new[] { "ClientId" });
            DropIndex("dbo.Subscriptions", new[] { "TariffId" });
            DropIndex("dbo.Subscriptions", new[] { "ClientId" });
            DropIndex("dbo.Schedule", new[] { "WorkShiftId" });
            DropIndex("dbo.Schedule", new[] { "TrainerId" });
            DropIndex("dbo.TrainingInGroup", new[] { "GroupTrainingId" });
            DropIndex("dbo.TrainingInGroup", new[] { "ClientId" });
            DropIndex("dbo.GroupTraining", new[] { "GroupTrainingTypeId" });
            DropIndex("dbo.GroupTraining", new[] { "RoomId" });
            DropIndex("dbo.GroupTraining", new[] { "TrainerId" });
            DropIndex("dbo.PersonalTrainings", new[] { "TrainerId" });
            DropTable("dbo.Trainings");
            DropTable("dbo.Tariffs");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.WorkShifts");
            DropTable("dbo.Schedule");
            DropTable("dbo.TrainingInGroup");
            DropTable("dbo.Rooms");
            DropTable("dbo.GroupTrainingTypes");
            DropTable("dbo.GroupTraining");
            DropTable("dbo.Trainers");
            DropTable("dbo.PersonalTrainings");
            DropTable("dbo.Clients");
        }
    }
}
