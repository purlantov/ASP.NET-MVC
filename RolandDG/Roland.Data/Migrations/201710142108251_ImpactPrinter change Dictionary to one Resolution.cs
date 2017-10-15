namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImpactPrinterchangeDictionarytooneResolution : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImpactPrinters", "Resolution", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ImpactPrinters", "Resolution");
        }
    }
}
