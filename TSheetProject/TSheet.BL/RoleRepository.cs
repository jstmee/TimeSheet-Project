using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class RoleRepository : IRole
    {
        TSheetDB db;
        RoleRepository()
        {
            db = new TSheetDB();
        }
        public int AddRoles(Role role)
        {
            db.Roles.Add(role); 
            return db.SaveChanges();
            
        }

        public IEnumerable<Role> GetAllRoles()
        { 
            var v=db.Roles.ToList();
            return v;
        }

        public Role GetRoleById(int Id)
        {
            var v=db.Roles.Where(a => a.RoleID == Id).FirstOrDefault();
            return v;
        }
    }
}

