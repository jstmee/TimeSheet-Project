using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSheet.Data;

namespace TSheet.IBL
{
    public interface IDescriptionAndProjectMapping
    {
        IList<DescriptionAndProjectMapping> GetAllDescriptionAndProjectMapping();

        int AddProject(DescriptionAndProjectMapping obj);

        DescriptionAndProjectMapping GetDescriptionAndProjectMapping(int id);

    }
}
