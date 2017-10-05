namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductstouser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Category = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.CreatedOn)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Products", new[] { "User_Id" });
            DropIndex("dbo.Products", new[] { "CreatedOn" });
            DropTable("dbo.Products");
        }
    }
}
