using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    public class TotalUserConuntController : Controller
    {

        TSheetDB dB = new TSheetDB();
        // GET: TotalUserConunt
        public ActionResult TotalUserCountsForAdmin()
        {
            var users = dB.AssignedRoles.Where(x => x.RoleID == 3).ToList();
            return View(users);
        }
    }
}