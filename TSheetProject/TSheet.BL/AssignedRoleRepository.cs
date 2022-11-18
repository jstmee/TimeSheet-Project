using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class AssignedRoleRepository : IAssignedRole
    {
        TSheetDB db;
        AssignedRoleRepository()
        {
            db=new TSheetDB();
        }         
        AssignedRoleRepository(TSheetDB _db )
        {
            db = _db;
        }

        public int AddAssignedRoles(AssignedRole Obj)
        {
            db.AssignedRoles.Add( Obj );
            return db.SaveChanges();

        }

        public IEnumerable<AssignedRole> GetAllAssignedRoles()
        {
            var v=db.AssignedRoles.ToList();
            return v;
        }

        public AssignedRole GetAssignedRoleById(int Id)
        {
            var v=db.AssignedRoles.Where(a=>a.AssignedRolesID==Id).FirstOrDefault();
            return v;
        }
    }
}
