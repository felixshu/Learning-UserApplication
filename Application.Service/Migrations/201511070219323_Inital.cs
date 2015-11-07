namespace Application.Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 20),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .Index(t => t.CategoryName, unique: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.InventoryItems",
                c => new
                    {
                        InventoryItemId = c.Int(nullable: false),
                        InventoryItemCode = c.String(nullable: false, maxLength: 15),
                        InventoryItemName = c.String(nullable: false, maxLength: 80),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryItemId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.InventoryItemCode, unique: true)
                .Index(t => t.InventoryItemName, unique: true)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        AccountNumber = c.String(nullable: false, maxLength: 8),
                        CompanyName = c.String(nullable: false, maxLength: 30),
                        Address = c.String(nullable: false, maxLength: 30),
                        City = c.String(nullable: false, maxLength: 15),
                        State = c.String(nullable: false, maxLength: 2),
                        ZipCode = c.String(nullable: false, maxLength: 10),
                        Phone = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.CustomerId)
                .Index(t => t.AccountNumber, unique: true, name: "IX_Customer_AccountName");
            
            CreateTable(
                "dbo.Labors",
                c => new
                    {
                        LaborId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        ServiceItemCode = c.String(nullable: false, maxLength: 15),
                        ServiceItemName = c.String(nullable: false, maxLength: 80),
                        LaborHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        //Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 140),
                    })
                .PrimaryKey(t => t.LaborId)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrderId, cascadeDelete: true)
                .Index(t => new { t.WorkOrderId, t.ServiceItemCode }, unique: true, name: "IX_Labor");

			Sql("ALTER TABLE dbo.Labors ADD Total AS CAST(LaborHours * Rate AS DECIMAL(18,2))");
            
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        WorkOrderId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        OrderDateTime = c.DateTime(nullable: false, defaultValueSql:"GetDate()"),
                        TargetDateTime = c.DateTime(),
                        DropDeadDateTime = c.DateTime(),
                        Description = c.String(maxLength: 256),
                        WorkOrderStatus = c.Int(nullable: false),
                        //TotalDue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CertificationRequirement = c.String(maxLength: 120),
                    })
                .PrimaryKey(t => t.WorkOrderId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
			Sql(@"CREATE FUNCTION dbo.SumOfPartsandLabors (@workOrderId AS INT)
					RETURNS DECIMAL(18,2)

					AS
					BEGIN
					DECLARE @sumOfParts DECIMAL(18,2), @sumOfLabors DECIMAL(18,2)

					SELECT @sumOfParts = SUM(Total)
					FROM dbo.Parts
					WHERE WorkOrderId = @workOrderId

					SELECT @sumOfLabors = SUM(Total)
					FROM dbo.Labors
					WHERE WorkOrderId = @workOrderId

							RETURN ISNULL(@sumOfLabors,0) + ISNULL(@sumOfParts,0)
					END");

			Sql("ALTER TABLE dbo.WorkOrders ADD TotalDue AS dbo.SumOfPartsandLabors(WorkOrderId)");
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        WorkOrderId = c.Int(nullable: false),
                        InventoryItemCode = c.String(nullable: false, maxLength: 15),
                        InventroyItemName = c.String(nullable: false, maxLength: 80),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        //Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 150),
                        IsInstalled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PartId)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrderId, cascadeDelete: true)
                .Index(t => t.WorkOrderId, unique: true, name: "IX_Part_WorkOrderId")
                .Index(t => t.InventoryItemCode, unique: true, name: "IX_Part_InventoryName");
            Sql("ALTER TABLE dbo.Parts ADD Total AS CAST(Quantity * UnitPrice AS DECIMAL(18,2))");


            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ServiceItems",
                c => new
                    {
                        ServiceItemId = c.Int(nullable: false),
                        ServiceItemCode = c.String(nullable: false, maxLength: 15),
                        ServiceItemName = c.String(nullable: false, maxLength: 80),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ServiceItemId)
                .Index(t => t.ServiceItemCode, unique: true, name: "IX_ServiceCode")
                .Index(t => t.ServiceItemName, unique: true, name: "IX_ServiceName");
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Parts", "WorkOrderId", "dbo.WorkOrders");
            DropForeignKey("dbo.Labors", "WorkOrderId", "dbo.WorkOrders");
            DropForeignKey("dbo.WorkOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.InventoryItems", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.ServiceItems", "IX_ServiceName");
            DropIndex("dbo.ServiceItems", "IX_ServiceCode");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Parts", "IX_Part_InventoryName");
            DropIndex("dbo.Parts", "IX_Part_WorkOrderId");
            DropIndex("dbo.WorkOrders", new[] { "CustomerId" });
            DropIndex("dbo.Labors", "IX_Labor");
            DropIndex("dbo.Customers", "IX_Customer_AccountName");
            DropIndex("dbo.InventoryItems", new[] { "CategoryId" });
            DropIndex("dbo.InventoryItems", new[] { "InventoryItemName" });
            DropIndex("dbo.InventoryItems", new[] { "InventoryItemCode" });
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Categories", new[] { "CategoryName" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parts");
            DropTable("dbo.WorkOrders");
            DropTable("dbo.Labors");
            DropTable("dbo.Customers");
            DropTable("dbo.InventoryItems");
            DropTable("dbo.Categories");
        }
    }
}
