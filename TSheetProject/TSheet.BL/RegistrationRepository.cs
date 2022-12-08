using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.IBL;
using TSheet.Data;
using TSheet.Models;
using System.Web;

namespace TSheet.BL
{
    public class RegistrationRepository : IRegistration
    {
        private TSheetDB sheetDB;
        
       public RegistrationRepository()
        {
            this.sheetDB = new TSheetDB();
            
        }
       

        public int AddRegistration(RegistrationModel user)
        {
            
            
            Registration registration = new Registration();
            registration.FirstName = user.FirstName;
            registration.LastName = user.LastName;
            registration.Email = user.Email;

            registration.DateOfJoining = user.DateOfJoining;
            
            registration.DateOfbirth = user.DateOfBirth;
       
            registration.Gender = user.Gender;
            registration.IsActive = true;
            registration.Password = Crypto.Hash(user.Password);
            registration.MobileNumber = user.MobileNumber;
            var LoggedUser = user.CreatedBy;
            
            var userrow = sheetDB.Registrations.Where(r => r.Email == LoggedUser).FirstOrDefault();
            var UserIdLogged = userrow.UserID.ToString();
            registration.CreatedBy = UserIdLogged;
            
            registration.CreatedOn=DateTime.Now;
            
            
            sheetDB.Registrations.Add(registration);
            
            return sheetDB.SaveChanges();
        }

        public IEnumerable<Registration> GetAllRegistrations()
        {
            IEnumerable<Registration> registrations = (from objRegister in sheetDB.Registrations
                                                       select new Registration()
                                                       {
                                                           UserID = objRegister.UserID,
                                                           FirstName = objRegister.FirstName,
                                                           LastName = objRegister.LastName,
                                                           Email = objRegister.Email,
                                                           Password = objRegister.Password,
                                                           MobileNumber = objRegister.MobileNumber,
                                                           DateOfbirth = objRegister.DateOfbirth,
                                                           Gender = objRegister.Gender,
                                                           CreatedOn = objRegister.CreatedOn,
                                                           CreatedBy = objRegister.CreatedBy,
                                                           IsActive = objRegister.IsActive,
                                                           DateOfJoining = objRegister.DateOfJoining,
                                                           DateOfLeaving = objRegister.DateOfLeaving

                                                       }).ToList();
            return registrations;
        }

        public Registration GetRegistrationByEmail(string Email)
        {
            Registration obj=sheetDB.Registrations.Where(x => x.Email == Email).FirstOrDefault();
            return obj;
        } 

        public Registration GetRegistrationById(int UserId)
        {
            Registration obj = sheetDB.Registrations.Where(x => x.UserID == UserId).FirstOrDefault();
            return obj;
        }
        public int EditUser(RegistrationModel user)
        {
            var v = GetRegistrationById(user.Id);
            v.Email = user.Email;
            v.DateOfbirth = user.DateOfBirth;
            v.DateOfJoining = user.DateOfJoining;
            v.FirstName = user.FirstName;
            v.LastName = user.LastName;
            v.Gender = user.Gender; 
            v.CreatedOn = user.CreatedOn;
            //v.CreatedBy = user.CreatedBy;
            v.DateOfLeaving = user.DateOfLeaving;

            return sheetDB.SaveChanges();

        }

       
    }
}
