using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;
using TSheet.IBL;

namespace TSheet.BL
{
    public class DescriptionAndProjectMappingRepository : IDescriptionAndProjectMapping
    {
        private TSheetDB sheetDB;
        public DescriptionAndProjectMappingRepository()
        {
            sheetDB = new TSheetDB();
        }
        public int AddProject(DescriptionAndProjectMapping obj)
        {
            throw new NotImplementedException();
        }

        public IList<DescriptionAndProjectMapping> GetAllDescriptionAndProjectMapping()
        {
            throw new NotImplementedException();
        }

        public DescriptionAndProjectMapping GetDescriptionAndProjectMapping(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProjectMaster> GetAllProjectsOfUser(int userid)
        {
            List<ProjectMaster> list = new List<ProjectMaster> ();

            var GetListOfProjects=sheetDB.DescriptionAndProjectMappings.Where(a=>a.UserID== userid).ToList();
            foreach ( var listItem in GetListOfProjects)
            {
                ProjectMaster projectMaster=new ProjectMaster();
                projectMaster.ProjectID = listItem.ProjectID;
                projectMaster.ProjectName = listItem.ProjectMaster.ProjectName;
                list.Add(projectMaster);
            }

            return list;

        }
    }
}
