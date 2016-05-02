using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SCM.Utils
{
    public class SecurityManager
    {
        private UserManager<ApplicationUser> UserManager { get; set; }
        private RoleManager<IdentityRole> RoleManager { get; set; }
        private ApplicationDbContext context;

        public SecurityManager()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        }

        public static void InitializeRolesAndUsers()
        {
            SecurityManager instance = new SecurityManager();
            instance.CreateRolesandUsers();
        }

        // In this method we will create default User roles and Admin user for login
        private void CreateRolesandUsers()
        {

            string Id = null;

            createRole("Admin");
            createRole("Supervisor");

            Id = CreateUser("Admin", "admin@scm.com", "0", "Admin@123");
            if (!string.IsNullOrEmpty(Id))
            {
                AddUserToRole(Id, "Admin");
                AddUserToRole(Id, "Supervisor");
            }

            Id = CreateUser("User101", "user101@scm.com", "101", "User101@123");
            if (!string.IsNullOrEmpty(Id))
            {
                AddUserToRole(Id, "Supervisor");
            }

            Id = CreateUser("User102", "user102@scm.com", "102", "User102@123");
            Id = CreateUser("User103", "user103@scm.com", "103", "User103@123");
            Id = CreateUser("User104", "user104@scm.com", "104", "User104@123");
            Id = CreateUser("User105", "user105@scm.com", "105", "User105@123");
        }

        public bool createRole(string roleName)
        {
            if (!RoleManager.RoleExists(roleName))
            {

                var role = new IdentityRole();
                role.Name = roleName;
                RoleManager.Create(role);
                return true;
            }

            return false;
        }
        public string CreateUser(string userName, string email, string ext, string password)
        {
            var user = new ApplicationUser();
            user.UserName = userName;
            user.Email = email;
            user.Ext = ext;

            string userPWD = password;
            var chkUser = UserManager.Create(user, userPWD);
            if (chkUser.Succeeded)
                return user.Id;
            else
                return null;
        }
        public void AddUserToRole(string userId, string role)
        {
            UserManager.AddToRole(userId, role);           
        }
        public string GetUserExt(string userName)
        {
            ApplicationUser user = UserManager.FindByName(userName);
            if (user != null)
            {
                return user.Ext;
            }
            else
                return "";
        }
    }
}