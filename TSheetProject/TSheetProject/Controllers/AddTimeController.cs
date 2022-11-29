using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TSheet.BL;
using TSheet.Models;

namespace TSheetProject.Controllers
{
    public class AddTimeController : Controller
    {
        private ProjectRepository _projectRepository;
        public AddTimeController()
        {
            _projectRepository=new ProjectRepository();
        }
        // GET: AddTime
        [HttpGet]
        public ActionResult AddTime()
        {
            List<AddTimeSheetModel> addTimeSheetModels = new List<AddTimeSheetModel>();
            var projectcount=_projectRepository.GetAllProjects().Count();
            for(int i=0;i<projectcount;i++)
            {
                AddTimeSheetModel addTimeSheetobj= new AddTimeSheetModel();
                addTimeSheetobj.id = i + 1;
                addTimeSheetModels.Add(addTimeSheetobj);
            }

            return View(addTimeSheetModels);
        }
        [HttpPost]
        public ActionResult AddTime(List<AddTimeSheetModel> addTime)
        {

            return View(addTime);
        }
    }
}