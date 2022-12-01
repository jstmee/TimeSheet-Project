using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class ProjectRepository : IProjectMaster
    {
        private TSheetDB sheetDB;
        public ProjectRepository()
        {
            sheetDB=new TSheetDB();
        }
        public int AddProject(ProjectMaster project)
        {
            sheetDB.ProjectMasters.Add(project);
            return sheetDB.SaveChanges();
        }

        public IList<ProjectMaster> GetAllProjects()
        {
            IList<ProjectMaster> projectMasters =sheetDB.ProjectMasters.ToList();
            return projectMasters;
        }

        public ProjectMaster GetProject(int id)
        {
            ProjectMaster project  = sheetDB.ProjectMasters.Where(a => a.ProjectID == id).SingleOrDefault();

            return project;
            
            
        }
    }

   
}
