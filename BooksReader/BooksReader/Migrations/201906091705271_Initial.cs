namespace BooksReader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Description = c.String(),
                        Genres = c.String(),
                        Pages = c.Int(),
                        Rating = c.Decimal(precision: 18, scale: 2),
                        RatingCount = c.Long(),
                        ReviewCount = c.Long(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateAdded = c.DateTime(nullable: false),
                        Message = c.String(),
                        Group_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Rules = c.String(),
                        Moderator_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Moderator_Id)
                .Index(t => t.Moderator_Id);
            
            CreateTable(
                "dbo.UserBookStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Book_Id = c.Int(),
                        BookStatus_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.BookStatus", t => t.BookStatus_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.BookStatus_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                        ImageUrl = c.String(),
                        Description = c.String(),
                        Blog = c.String(),
                        Gender = c.String(),
                        Interests = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBookStatus", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserBookStatus", "BookStatus_Id", "dbo.BookStatus");
            DropForeignKey("dbo.UserBookStatus", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "Moderator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserProfiles", new[] { "UserId" });
            DropIndex("dbo.UserBookStatus", new[] { "User_Id" });
            DropIndex("dbo.UserBookStatus", new[] { "BookStatus_Id" });
            DropIndex("dbo.UserBookStatus", new[] { "Book_Id" });
            DropIndex("dbo.Groups", new[] { "Moderator_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Group_Id" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.UserBookStatus");
            DropTable("dbo.Groups");
            DropTable("dbo.Comments");
            DropTable("dbo.BookStatus");
            DropTable("dbo.Books");
        }
    }
}
