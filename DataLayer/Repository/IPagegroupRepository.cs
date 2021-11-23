using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
   public interface IPagegroupRepository:IDisposable
    {
        IEnumerable<PageGroup> GetAllPagegroup();
        PageGroup GetGroupByID(int groupID);
        bool InsertGroup(PageGroup pagegroup);
        bool UpdateGroup(PageGroup pagegroup);
        bool DeleteGroup(PageGroup pagegroup);
        bool DeleteGroup(int groupID);
        void Save();
        IEnumerable<ShowGroupViewModel> GetGroupForView();

    }
}
