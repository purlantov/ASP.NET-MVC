namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Printer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Printers",
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Printers", new[] { "CreatedOn" });
            DropTable("dbo.Printers");
        }
    }
}
