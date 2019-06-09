namespace BooksReader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserProfiles", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
