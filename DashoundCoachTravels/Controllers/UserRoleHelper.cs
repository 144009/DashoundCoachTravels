using DashoundCoachTravels.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

// https://stackoverflow.com/questions/11515973/where-can-i-put-custom-classes-in-asp-net-mvc#11516018
// this class will be used  in other controllers to help manage use roles: admin, normal user, emplyee
// with its help it will be possible to check if an user has a correct role and if a controller method can be run
// by an user with certain role
namespace DashoundCoachTravels.Helpers
{
    public static class UserRoleHelper
    {
        // returns bool if a specific id-user has a specific role type
        // https://docs.microsoft.com/en-us/dotnet/api/system.web.security.roles.isuserinrole?view=netframework-4.7.2#System_Web_Security_Roles_IsUserInRole_System_String_System_String_
        public static bool IsUserInRole(string UserId, string RoleType)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userStore = new UserStore<ApplicationUser>(db);
            var userManager = new UserManager<ApplicationUser>(userStore);
            if (userManager.FindById(UserId) == null) // if specified user doesnt exist at all return false
                return false;
            return userManager.IsInRole(UserId, RoleType); //otherwise check if specified user has a role
        }

        // here the specific user role types to be checked using above method
        public static bool IsAdmin(string userid)
        {
            return IsUserInRole(userid, "Administrator");
        }

        public static bool IsEmployee(string userid)
        {
            return IsUserInRole(userid, "Employee");
        }

        public static bool IsUser(string userid)
        {
            return IsUserInRole(userid, "User");
        }
    }
}