namespace BookReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentrevival : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Text = c.String(nullable: false),
                    PostedOn = c.DateTime(nullable: false),  
                    AspNetUser_Id = c.String(nullable: false, maxLength: 128),
                    Book_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.AspNetUser_Id)
                .Index(t => t.Book_Id);
        }
        
        public override void Down()
        {
        }
    }
}
