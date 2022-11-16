using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.Modals;

namespace TSheet.IBL
{
    public interface IRole
    {
        IEnumerable<Role> GetAllRoles();

        int AddRoles(Role role);

        Role GetRoleById(int RoleId);



        
    }
}
