﻿namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_DataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        PageID = c.Int(nullable: false, identity: true),
                        GroupID = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 250),
                        ShortDiscription = c.String(nullable: false, maxLength: 350),
                        Text = c.String(nullable: false),
                        Visit = c.Int(nullable: false),
                        ImageName = c.String(),
                        ShowInSlider = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PageID)
                .ForeignKey("dbo.PageGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.PageComments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        PageID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(maxLength: 200),
                        Website = c.String(maxLength: 200),
                        Comment = c.String(nullable: false, maxLength: 500),
                        CreateDateComment = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Pages", t => t.PageID, cascadeDelete: true)
                .Index(t => t.PageID);
            
            CreateTable(
                "dbo.PageGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GruopTitle = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.GroupID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pages", "GroupID", "dbo.PageGroups");
            DropForeignKey("dbo.PageComments", "PageID", "dbo.Pages");
            DropIndex("dbo.PageComments", new[] { "PageID" });
            DropIndex("dbo.Pages", new[] { "GroupID" });
            DropTable("dbo.PageGroups");
            DropTable("dbo.PageComments");
            DropTable("dbo.Pages");
        }
    }
}