using VRMSTT.Api.Infrastructure;
using VRMSTT.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Http;
using System;

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
                CreatedDate =DateTime.Now
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

        protected override void Dispose(bool disposing)
        {
            _userManager.Dispose();
        }
    }
}