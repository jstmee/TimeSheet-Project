using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.Modals
{
    public class ProjectsModel
    {
        public int ProjectId { get; set; }
        [Display(Name ="Project Name")]
        public string ProjectName { get; set; }
        [Display(Name ="Add Project Description")]
        public string ProjectDescription { get; set; }
    }
}
