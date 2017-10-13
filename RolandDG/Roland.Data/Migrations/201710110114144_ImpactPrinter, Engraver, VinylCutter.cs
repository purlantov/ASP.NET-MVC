namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImpactPrinterEngraverVinylCutter : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Engravers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                        ProductType = c.Int(nullable: false),
                        MaxSpeed = c.Int(nullable: false),
                        RPM = c.Int(nullable: false),
                        TableWidth = c.Int(nullable: false),
                        TableDepth = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreatedOn);
            
            CreateTable(
                "dbo.ImpactPrinters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                        ProductType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreatedOn);
            
            CreateTable(
                "dbo.VinylCutters",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Model = c.String(nullable: false, maxLength: 30),
                        ProductType = c.Int(nullable: false),
                        CuttingSpeed = c.Int(nullable: false),
                        BladeForce = c.Int(nullable: false),
                        MediaWidth = c.Int(nullable: false),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CreatedOn);
            
            AddColumn("dbo.Printers", "PrintHeads", c => c.Int(nullable: false));
            AddColumn("dbo.Printers", "MediaWidth", c => c.Int(nullable: false));
            AddColumn("dbo.Printers", "Ink", c => c.Int(nullable: false));
            AddColumn("dbo.Printers", "MaxSpeed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropIndex("dbo.VinylCutters", new[] { "CreatedOn" });
            DropIndex("dbo.ImpactPrinters", new[] { "CreatedOn" });
            DropIndex("dbo.Engravers", new[] { "CreatedOn" });
            DropColumn("dbo.Printers", "MaxSpeed");
            DropColumn("dbo.Printers", "Ink");
            DropColumn("dbo.Printers", "MediaWidth");
            DropColumn("dbo.Printers", "PrintHeads");
            DropTable("dbo.VinylCutters");
            DropTable("dbo.ImpactPrinters");
            DropTable("dbo.Engravers");
        }
    }
}
