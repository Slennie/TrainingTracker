using VRMSTT.Api.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace VRMSTT.Api.Infrastructure
{
    public class VRMSTTDataContext : IdentityDbContext<User>
    {
        public VRMSTTDataContext() : base("VRMSTT")
        {

        }
       
        public IDbSet<Notification> Notifications { get; set; }
        public IDbSet<Enrollment> Enrollments { get; set; }
        public IDbSet<Department> Departments { get; set; }
        public IDbSet<Course> Courses { get; set; }
        public IDbSet<Certificate> Certificates { get; set; }
        public IDbSet<CertificateItem> CertificateItems { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Many to One Relations 
            modelBuilder.Conventions.Remove <PluralizingTableNameConvention>();

            // 1 Department has many Users
            modelBuilder.Entity<Department>()
                        .HasMany(d => d.Users)
                        .WithRequired(u => u.Department)
                        .HasForeignKey(u => u.DepartmentId)
                        .WillCascadeOnDelete(false);

            // 1 User has many Enrollments
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Enrollments)
                        .WithRequired(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .WillCascadeOnDelete(false);


            // 1 User has many Notifications
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Notifications)
                        .WithRequired(n => n.User)
                        .HasForeignKey(n => n.UserId);
            
            // 1 User has many Certificates
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Certificates)
                        .WithRequired(c => c.User)
                        .HasForeignKey(c => c.UserId);

            // 1 User has many CREATED Courses
            modelBuilder.Entity<User>()
                        .HasMany(u => u.Courses)
                        .WithRequired(c => c.User)
                        .HasForeignKey(c => c.UserId)
                        .WillCascadeOnDelete(false);

            // 1 Course has many Enrollments
            modelBuilder.Entity<Course>()
                        .HasMany(c => c.Enrollments)
                        .WithRequired(e => e.Course)
                        .HasForeignKey(e => e.CourseId)
                        .WillCascadeOnDelete(false);

            // 1 Certificate has many CertItems
            modelBuilder.Entity<Certificate>()
                        .HasMany(c => c.CertificateItems)
                        .WithOptional(ci => ci.Certificate)
                        .HasForeignKey(ci => ci.CertificateId);

            //Compound Key
            //enrollment => many users + many courses
            modelBuilder.Entity<Enrollment>()
                        .HasKey(e => new { e.UserId, e.CourseId })
                        .WillCascadeOnDelete(false);

            //Kicks back to base model builder after connecting relations
            base.OnModelCreating(modelBuilder);
            
            
        }
    }
}