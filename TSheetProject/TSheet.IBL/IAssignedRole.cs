using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    public interface IAssignedRole
    {
        IEnumerable<AssignedRole> GetAllAssignedRoles();

        int AddAssignedRoles(AssignedRole Obj);

        AssignedRole GetAssignedRoleById(int RoleId);




    }
}
