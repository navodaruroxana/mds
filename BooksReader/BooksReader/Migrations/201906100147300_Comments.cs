namespace BooksReader.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comments : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "DateAdded", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
