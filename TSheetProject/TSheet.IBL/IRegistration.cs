using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.Modals;

namespace TSheet.IBL
{
    public interface IRegistration
    {
        IEnumerable<Registration> GetAllRegistrations();

        int AddRegistration(RegistrationModel User);

        Registration GetRegistrationById(int UserId);

        

        Registration GetRegistrationByEmail(string Email);



    }
}
