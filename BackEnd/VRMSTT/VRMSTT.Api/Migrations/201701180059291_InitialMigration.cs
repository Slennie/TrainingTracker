namespace VRMSTT.Api.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CertificateItems",
                c => new
                    {
                        CertificateItemId = c.Int(nullable: false, identity: true),
                        CertificateId = c.Int(),
                        Name = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.CertificateItemId)
                .ForeignKey("dbo.Certificates", t => t.CertificateId)
                .Index(t => t.CertificateId);
            
            CreateTable(
                "dbo.Certificates",
                c => new
                    {
                        CertificateId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        CertDateIssued = c.DateTime(nullable: false),
                        CertExpirationDate = c.DateTime(),
                        IssuedBy = c.String(),
                    })
                .PrimaryKey(t => t.CertificateId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DepartmentId = c.Int(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        Gender = c.String(),
                        Ethnicity = c.String(),
                        JobTitle = c.String(),
                        Rank = c.String(),
                        Unit = c.String(),
                        PrimaryShift = c.String(),
                        Timezone = c.String(),
                        DOB = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        TempPreference = c.String(),
                        SpeedPreference = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        PrimaryJobTitle_PrimaryJobTitleId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .ForeignKey("dbo.PrimaryJobTitles", t => t.PrimaryJobTitle_PrimaryJobTitleId)
                .Index(t => t.DepartmentId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.PrimaryJobTitle_PrimaryJobTitleId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Instructor = c.String(),
                        CourseTitle = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        location = c.String(),
                        PPERequired = c.Boolean(),
                        MaterialsNeeded = c.String(),
                        SpecialInstructions = c.String(),
                        AvailableOnline = c.Boolean(),
                        CertificateAwarded = c.String(),
                        EMSCE = c.Int(nullable: false),
                        CertificateExpiration = c.DateTime(),
                        Cost = c.String(),
                        PaymentOption = c.String(),
                        HostingOrganization = c.String(),
                        ReimbursingAgency = c.String(),
                        LabOrLecture = c.String(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CourseId })
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        DateSeen = c.DateTime(),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PrimaryJobTitles",
                c => new
                    {
                        PrimaryJobTitleId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.PrimaryJobTitleId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "PrimaryJobTitle_PrimaryJobTitleId", "dbo.PrimaryJobTitles");
            DropForeignKey("dbo.Notifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Courses", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Enrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Certificates", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CertificateItems", "CertificateId", "dbo.Certificates");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Enrollments", new[] { "CourseId" });
            DropIndex("dbo.Enrollments", new[] { "UserId" });
            DropIndex("dbo.Courses", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "PrimaryJobTitle_PrimaryJobTitleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "DepartmentId" });
            DropIndex("dbo.Certificates", new[] { "UserId" });
            DropIndex("dbo.CertificateItems", new[] { "CertificateId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.PrimaryJobTitles");
            DropTable("dbo.Notifications");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Departments");
            DropTable("dbo.Enrollments");
            DropTable("dbo.Courses");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Certificates");
            DropTable("dbo.CertificateItems");
        }
    }
}
