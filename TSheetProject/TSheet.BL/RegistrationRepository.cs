﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.IBL;
using TSheet.Data;
using TSheet.Modals;

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
            registration.EditedBy = user.EditedBy;
            registration.CreatedBy = user.CreatedBy;
            registration.DateOfJoining = user.DateOfJoining;
            registration.DateOfLeaving = user.DateOfLeaving;
            registration.DateOfbirth = user.DateOfBirth;
            registration.CreatedOn = user.CreatedOn;
            registration.Gender = user.Gender;
            registration.IsActive = user.IsActive;
            registration.Password = user.Password;
            registration.MobileNumber = user.MobileNumber;
            registration.UpdatedOn = user.UpdatedOn;
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

       
    }
}