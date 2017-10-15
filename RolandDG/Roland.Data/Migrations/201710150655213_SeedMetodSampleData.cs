namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedMetodSampleData : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Engravers", "ProductType", c => c.String(nullable: false));
            AlterColumn("dbo.ImpactPrinters", "ProductType", c => c.String(nullable: false));
            AlterColumn("dbo.Printers", "ProductType", c => c.String(nullable: false));
            AlterColumn("dbo.VinylCutters", "ProductType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VinylCutters", "ProductType", c => c.Int(nullable: false));
            AlterColumn("dbo.Printers", "ProductType", c => c.Int(nullable: false));
            AlterColumn("dbo.ImpactPrinters", "ProductType", c => c.Int(nullable: false));
            AlterColumn("dbo.Engravers", "ProductType", c => c.Int(nullable: false));
        }
    }
}
