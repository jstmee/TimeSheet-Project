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
            var v=sheetDB.ProjectMasters.ToList();
            return v;
        }

        public ProjectMaster GetProject(int id)
        {
            throw new NotImplementedException();
        }
    }

   
}
