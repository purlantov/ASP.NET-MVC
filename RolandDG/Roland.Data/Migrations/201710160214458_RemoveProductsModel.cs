namespace Roland.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProductsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "User_Id", "dbo.Users");
            DropIndex("dbo.Products", new[] { "CreatedOn" });
            DropIndex("dbo.Products", new[] { "User_Id" });
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Products", "User_Id");
            CreateIndex("dbo.Products", "CreatedOn");
            AddForeignKey("dbo.Products", "User_Id", "dbo.Users", "Id");
        }
    }
}
