namespace VRMSTT.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCreatedDateForModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CertificateItem", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Department", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Notification", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notification", "DateSent");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Notification", "DateSent", c => c.DateTime(nullable: false));
            DropColumn("dbo.Notification", "CreatedDate");
            DropColumn("dbo.Department", "CreatedDate");
            DropColumn("dbo.AspNetUsers", "CreatedDate");
            DropColumn("dbo.CertificateItem", "CreatedDate");
        }
    }
}
