namespace BookReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class unique_bookAspNetUser : DbMigration
    {
        public override void Up()
        {
            CreateIndex("BookAspNetUsers", new string[] { "AspNetUserId", "BookId" }, true, "UniqueFavBook");
        }
        
        public override void Down()
        {
        }
    }
}
