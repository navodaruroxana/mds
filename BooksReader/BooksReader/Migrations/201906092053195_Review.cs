namespace BooksReader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Review : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserBookRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Book_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserBookReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Review = c.String(),
                        Book_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserBookReviews", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBookReviews", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.UserBookRatings", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBookRatings", "Book_Id", "dbo.Books");
            DropIndex("dbo.UserBookReviews", new[] { "User_Id" });
            DropIndex("dbo.UserBookReviews", new[] { "Book_Id" });
            DropIndex("dbo.UserBookRatings", new[] { "User_Id" });
            DropIndex("dbo.UserBookRatings", new[] { "Book_Id" });
            DropTable("dbo.UserBookReviews");
            DropTable("dbo.UserBookRatings");
        }
    }
}
