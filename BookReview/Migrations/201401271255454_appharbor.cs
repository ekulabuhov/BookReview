namespace BookReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appharbor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        FullName = c.String(),
                        Discriminator = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .Index(t => t.AspNetUser_Id);
            
            CreateTable(
                "dbo.BookAspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AspNetUserId = c.String(maxLength: 128),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUserId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.AspNetUserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(),
                        Title = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Author = c.String(),
                        Url = c.String(),
                        Picture = c.String(),
                        Publisher = c.String(),
                        ISBN = c.String(),
                        Language = c.String(),
                        Binding = c.String(),
                        Page_extent = c.String(),
                        Barcode = c.String(),
                        Series = c.String(),
                        OzonBookId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PostedOn = c.DateTime(nullable: false),
                        BooksId = c.Int(nullable: false),
                        AspNetUsersId = c.String(),
                        AspNetUser_Id = c.String(maxLength: 128),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.AspNetUser_Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .Index(t => t.AspNetUser_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Comments", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Comments", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookAspNetUsers", "BookId", "dbo.Books");
            DropForeignKey("dbo.BookAspNetUsers", "AspNetUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "AspNetUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "AspNetUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Comments", new[] { "Book_Id" });
            DropIndex("dbo.Comments", new[] { "AspNetUser_Id" });
            DropIndex("dbo.BookAspNetUsers", new[] { "BookId" });
            DropIndex("dbo.BookAspNetUsers", new[] { "AspNetUserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "AspNetUser_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "AspNetUser_Id" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Comments");
            DropTable("dbo.Books");
            DropTable("dbo.BookAspNetUsers");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
        }
    }
}
