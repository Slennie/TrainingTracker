using VRMSTT.Api.Infrastructure;
using VRMSTT.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;
using System;
using System.Web.Http.Description;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace VRMSTT.Api.Controllers
{
    public class UsersController : ApiController
    {
        private UserManager<User> _userManager;
        private VRMSTTDataContext db;
        public UsersController()
        {
            db = new VRMSTTDataContext();
            var store = new UserStore<User>(db);

            _userManager = new UserManager<User>(store);
        }

        // GET: api/Users
        public IHttpActionResult GetUsers()
        {
            return Ok(db.Users);
        }

        // GET: api/User/5
        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(string id)
        {
            User User = db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }
            var resultSet = new
            {
                //IdentityUser
                User.Id,
                User.Email,
                User.UserName,
                User.PhoneNumber,

                //User
                User.FirstName,
                User.MiddleName,
                User.LastName,
                User.Gender,
                User.Ethnicity,
                User.Rank,
                User.Unit,
                User.PrimaryShift,
                User.Timezone,
                User.DOB,
                User.TempPreference,
                User.SpeedPreference,
                User.StartDate,
                User.EndDate,
                User.CreatedDate,
                Certificates = User.Certificates.Select(c => new
                {
                    c.CertificateId,
                    c.Title,
                    c.CertDateIssued,
                    c.CertExpirationDate,
                    c.IssuedBy
                }),
                //Courses Created
                Courses = User.Courses.Select(cc => new
                {
                    cc.CourseId,
                    cc.CourseTitle,
                    cc.StartDate,
                    cc.EndDate
                }),
                Enrollments = User.Enrollments.Select(e => new
                {
                    e.Course.CourseId,
                    e.Course.CourseTitle,
                    e.Course.StartDate,
                    e.Course.EndDate
                }),
                Notifications = User.Notifications.Select(n => new
                {
                    n.NotificationId,
                    n.Text,
                    n.CreatedDate,
                    n.DateSeen
                })


            };
            return Ok(resultSet);
        }

        // GET: api/me
        [Authorize, Route("api/me")]
        public IHttpActionResult GetMe()
        {
            string userName = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.UserName == userName);
            var resultSet = new
            {
                //IdentityUser
                user.Id,
                user.Email,
                user.UserName,
                user.PhoneNumber,

                //User
                user.FirstName,
                user.MiddleName,
                user.LastName,
                user.Gender,
                user.Ethnicity,
                user.Rank,
                user.Unit,
                user.PrimaryShift,
                user.Timezone,
                user.DOB,
                user.TempPreference,
                user.SpeedPreference,
                user.StartDate,
                user.EndDate,
                user.CreatedDate,
                Certificates = user.Certificates.Select(c => new
                {
                    c.CertificateId,
                    c.Title,
                    c.CertDateIssued,
                    c.CertExpirationDate,
                    c.IssuedBy
                }),
                //Courses Created
                Courses = user.Courses.Select(cc => new
                {
                    cc.CourseId,
                    cc.CourseTitle,
                    cc.StartDate,
                    cc.EndDate
                }),
                Enrollments = user.Enrollments.Select(e => new
                {
                    e.Course.CourseId,
                    e.Course.CourseTitle,
                    e.Course.StartDate,
                    e.Course.EndDate
                }),
                Notifications = user.Notifications.Select(n => new
                {
                    n.NotificationId,
                    n.Text,
                    n.CreatedDate,
                    n.DateSeen
                })
            };
            return Ok(resultSet);
        }

        // GET: api/me/notifications/unseen
        [Authorize, Route("api/me/notifications/unseen")]
        public IHttpActionResult GetMyUnseenNotifications()
        {
            var resultSet = db.Notifications
                              .Where(n => !n.DateSeen.HasValue && n.User.UserName == User.Identity.Name)
                              .Select(n => new
                              {
                                  n.NotificationId,
                                  n.CreatedDate,
                                  n.DateSeen,
                                  n.Text
                              });

            return Ok(resultSet);
        }

        // POST: api/users/register
        [AllowAnonymous]
        [Route("api/users/register")]
        public IHttpActionResult Register(RegistrationModel registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var department = new Department
            {
                DepartmentName = registration.DepartmentName,
                Address = registration.Address,
                City = registration.City,
                State = registration.State,
                Zip = registration.Zip,
                CreatedDate = DateTime.Now

            };

            db.Departments.Add(department);
            db.SaveChanges();

            var user = new User
            {
                UserName = registration.EmailAddress,
                Department = department,
                CreatedDate = DateTime.Now
            };

            var result = _userManager.Create(user, registration.Password);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Invalid user registration");
            }
        }

        // PUT: api/me
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != user.Id)
            {
                return BadRequest();
            }
            // This is how we update stuff

            var dbUser = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);

            dbUser.UserName = user.UserName;
            dbUser.Email = user.Email;
            dbUser.DOB = user.DOB;
            dbUser.DepartmentId = user.DepartmentId;
            dbUser.FirstName = user.FirstName;
            dbUser.MiddleName = user.MiddleName;
            dbUser.LastName = user.LastName;
            dbUser.Gender = user.Gender;
            dbUser.Ethnicity = user.Ethnicity;
            dbUser.Rank = user.Rank;
            dbUser.Unit = user.Unit;
            dbUser.PrimaryShift = user.PrimaryShift;
            dbUser.Timezone = user.Timezone;
            dbUser.TempPreference = user.TempPreference;
            dbUser.SpeedPreference = user.SpeedPreference;

            db.Entry(dbUser).State = System.Data.Entity.EntityState.Modified;


            db.SaveChanges();

            return StatusCode(System.Net.HttpStatusCode.NoContent);

        }

        //DELETE: api/User/5
        [Authorize]
        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(string id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }

        // POST: api/user/5/certificate
        [HttpPost, Route("api/users/{id}/certificate")]
        public IHttpActionResult PostCertificate(string userId, int certificateId)
        {
            Certificate certificate = new Certificate();

            certificate.CertificateId = certificateId;
            certificate.UserId = userId;
            db.Certificates.Add(certificate);

            db.SaveChanges();

            return Ok();
        }


        //Delete Certificate of User
        [HttpDelete, Route("api/user/{id}/certificate")]
        public IHttpActionResult DeleteCertificateFromUser(string userId, int certificateId)
        {
            var certificate = db.Certificates.Find(userId, certificateId);
            db.Certificates.Remove(certificate);
            db.SaveChanges();

            return Ok();
        }










        protected override void Dispose(bool disposing)
        {
            _userManager.Dispose();
        }
    }
}