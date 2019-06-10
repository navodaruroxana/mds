namespace BooksReader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Group : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Group_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
            AddColumn("dbo.Groups", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Group_Id1", c => c.Int());
            CreateIndex("dbo.Groups", "ApplicationUser_Id");
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            CreateIndex("dbo.AspNetUsers", "Group_Id1");
            AddForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_Id1", "dbo.Groups");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Groups", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserGroups", new[] { "User_Id" });
            DropIndex("dbo.UserGroups", new[] { "Group_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.Groups", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Group_Id1");
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropColumn("dbo.Groups", "ApplicationUser_Id");
            DropTable("dbo.UserGroups");
        }
    }
}
