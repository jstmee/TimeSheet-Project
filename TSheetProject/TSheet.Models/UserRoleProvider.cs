using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using TSheet.Data;

namespace TSheet.Models
{
    public class UserRoleProvider :RoleProvider
    {
        public override string ApplicationName 
        { 
            get => throw new NotImplementedException(); set => throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string Email)
        {
            using(TSheetDB dB= new TSheetDB())
            {
                var userRoles=(from Role in dB.Roles join AssignedRole in dB.AssignedRoles on Role.RoleID equals AssignedRole.RoleID             join Registration in dB.Registrations on AssignedRole.UserID equals Registration.UserID
                               where Registration.Email == Email select Role.RoleName).ToArray();
                return userRoles;
            }

           
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
