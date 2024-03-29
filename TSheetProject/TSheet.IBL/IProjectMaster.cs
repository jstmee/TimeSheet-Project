﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    public interface IProjectMaster
    {
        IList<ProjectMaster> GetAllProjects();

        int AddProject(ProjectMaster project);

        ProjectMaster GetProject(int id);

    }
}
