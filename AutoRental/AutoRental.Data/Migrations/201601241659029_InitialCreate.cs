namespace AutoRental.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        AccountId = c.Int(nullable: false, identity: true),
                        LoginEmail = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        CreditCard = c.String(),
                        ExpDate = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AccountId);
            
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        VIN = c.String(),
                        Color = c.String(),
                        Year = c.Int(nullable: false),
                        RentalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PurchaseDate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CarId);
            
            CreateTable(
                "dbo.Rental",
                c => new
                    {
                        RentalId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        DateRented = c.DateTime(nullable: false),
                        DateDue = c.DateTime(nullable: false),
                        DateReturned = c.DateTime(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RentalId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        RentalDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        AccountId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        SaleAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sale");
            DropTable("dbo.Reservation");
            DropTable("dbo.Rental");
            DropTable("dbo.Car");
            DropTable("dbo.Account");
        }
    }
}
