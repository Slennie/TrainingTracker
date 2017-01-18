namespace VRMSTT.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepartmentInfoToRegistration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Department", "DepartmentName", c => c.String());
            DropColumn("dbo.Department", "Name");
            DropColumn("dbo.Department", "Location");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Department", "Location", c => c.String());
            AddColumn("dbo.Department", "Name", c => c.String());
            DropColumn("dbo.Department", "DepartmentName");
        }
    }
}
