namespace BoVoyageFinalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingFiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        TravelId = c.Int(nullable: false),
                        CreditCardNumber = c.String(nullable: false),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TravellersNumber = c.Int(nullable: false),
                        IsCustomerTraveller = c.Boolean(nullable: false),
                        BookingFileState = c.String(nullable: false),
                        BookingFileCancellationReason = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: false)
                .ForeignKey("dbo.Travels", t => t.TravelId, cascadeDelete: false)
                .Index(t => t.CustomerId)
                .Index(t => t.TravelId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PhoneNumber = c.String(nullable: false),
                        AddressLine1 = c.String(nullable: false, maxLength: 100),
                        AddressLine2 = c.String(maxLength: 100),
                        ZIPCode = c.String(nullable: false, maxLength: 10),
                        City = c.String(nullable: false, maxLength: 50),
                        Country = c.String(nullable: false, maxLength: 50),
                        Mail = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 150),
                        Title = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Mail, unique: true);
            
            CreateTable(
                "dbo.Insurances",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InsuranceCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InsuranceType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Travels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateGo = c.DateTime(nullable: false),
                        DateBack = c.DateTime(nullable: false),
                        SpaceAvailable = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TravelAgencyID = c.Int(nullable: false),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: false)
                .ForeignKey("dbo.TravelAgencies", t => t.TravelAgencyID, cascadeDelete: false)
                .Index(t => t.TravelAgencyID)
                .Index(t => t.DestinationID);
            
            CreateTable(
                "dbo.Destinations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Continent = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Area = c.String(nullable: false),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.DestinationPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        ContentType = c.String(nullable: false, maxLength: 20),
                        Content = c.Binary(nullable: false),
                        DestinationID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Destinations", t => t.DestinationID, cascadeDelete: false)
                .Index(t => t.DestinationID);
            
            CreateTable(
                "dbo.TravelAgencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Travellers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BirthDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        PhoneNumber = c.String(nullable: false),
                        Mail = c.String(maxLength: 150),
                        Title = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Mail, unique: true);
            
            CreateTable(
                "dbo.SalesManagers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Mail = c.String(nullable: false, maxLength: 150),
                        Password = c.String(nullable: false, maxLength: 150),
                        Title = c.String(),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Mail, unique: true);
            
            CreateTable(
                "dbo.InsuranceBookingFiles",
                c => new
                    {
                        Insurance_ID = c.Int(nullable: false),
                        BookingFile_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Insurance_ID, t.BookingFile_ID })
                .ForeignKey("dbo.Insurances", t => t.Insurance_ID, cascadeDelete: false)
                .ForeignKey("dbo.BookingFiles", t => t.BookingFile_ID, cascadeDelete: false)
                .Index(t => t.Insurance_ID)
                .Index(t => t.BookingFile_ID);
            
            CreateTable(
                "dbo.TravellerBookingFiles",
                c => new
                    {
                        Traveller_ID = c.Int(nullable: false),
                        BookingFile_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Traveller_ID, t.BookingFile_ID })
                .ForeignKey("dbo.Travellers", t => t.Traveller_ID, cascadeDelete: false)
                .ForeignKey("dbo.BookingFiles", t => t.BookingFile_ID, cascadeDelete: false)
                .Index(t => t.Traveller_ID)
                .Index(t => t.BookingFile_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TravellerBookingFiles", "BookingFile_ID", "dbo.BookingFiles");
            DropForeignKey("dbo.TravellerBookingFiles", "Traveller_ID", "dbo.Travellers");
            DropForeignKey("dbo.Travels", "TravelAgencyID", "dbo.TravelAgencies");
            DropForeignKey("dbo.Travels", "DestinationID", "dbo.Destinations");
            DropForeignKey("dbo.DestinationPictures", "DestinationID", "dbo.Destinations");
            DropForeignKey("dbo.BookingFiles", "TravelId", "dbo.Travels");
            DropForeignKey("dbo.InsuranceBookingFiles", "BookingFile_ID", "dbo.BookingFiles");
            DropForeignKey("dbo.InsuranceBookingFiles", "Insurance_ID", "dbo.Insurances");
            DropForeignKey("dbo.BookingFiles", "CustomerId", "dbo.Customers");
            DropIndex("dbo.TravellerBookingFiles", new[] { "BookingFile_ID" });
            DropIndex("dbo.TravellerBookingFiles", new[] { "Traveller_ID" });
            DropIndex("dbo.InsuranceBookingFiles", new[] { "BookingFile_ID" });
            DropIndex("dbo.InsuranceBookingFiles", new[] { "Insurance_ID" });
            DropIndex("dbo.SalesManagers", new[] { "Mail" });
            DropIndex("dbo.Travellers", new[] { "Mail" });
            DropIndex("dbo.DestinationPictures", new[] { "DestinationID" });
            DropIndex("dbo.Travels", new[] { "DestinationID" });
            DropIndex("dbo.Travels", new[] { "TravelAgencyID" });
            DropIndex("dbo.Customers", new[] { "Mail" });
            DropIndex("dbo.BookingFiles", new[] { "TravelId" });
            DropIndex("dbo.BookingFiles", new[] { "CustomerId" });
            DropTable("dbo.TravellerBookingFiles");
            DropTable("dbo.InsuranceBookingFiles");
            DropTable("dbo.SalesManagers");
            DropTable("dbo.Travellers");
            DropTable("dbo.TravelAgencies");
            DropTable("dbo.DestinationPictures");
            DropTable("dbo.Destinations");
            DropTable("dbo.Travels");
            DropTable("dbo.Insurances");
            DropTable("dbo.Customers");
            DropTable("dbo.BookingFiles");
        }
    }
}
