using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.Data;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    public class AddProjectForAdminController : Controller
    {
        // GET: AddProjectForAdmin
        public ActionResult AddProjects()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProjects(ProjectsModel projects)
        {
            string message = "";
            using (TSheetDB dB = new TSheetDB())
            {
                ProjectMaster projectMaster = new ProjectMaster();
                projectMaster.ProjectName = projects.ProjectName;
                projectMaster.ProjectDescription = projects.ProjectDescription;
                dB.ProjectMasters.Add(projectMaster);
                dB.SaveChanges();
                message = "Project added";

            }
            ViewBag.Message = message;
            return View(projects);
        }
    }
}