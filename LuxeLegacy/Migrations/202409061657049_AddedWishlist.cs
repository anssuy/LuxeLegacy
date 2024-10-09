namespace LuxeLegacy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWishlist : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WishlistItems",
                c => new
                    {
                        WishlistItemId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        WishlistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WishlistItemId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Wishlists", t => t.WishlistId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.WishlistId);
            
            CreateTable(
                "dbo.Wishlists",
                c => new
                    {
                        WishlistId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.WishlistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishlistItems", "WishlistId", "dbo.Wishlists");
            DropForeignKey("dbo.WishlistItems", "ProductId", "dbo.Products");
            DropIndex("dbo.WishlistItems", new[] { "WishlistId" });
            DropIndex("dbo.WishlistItems", new[] { "ProductId" });
            DropTable("dbo.Wishlists");
            DropTable("dbo.WishlistItems");
        }
    }
}
