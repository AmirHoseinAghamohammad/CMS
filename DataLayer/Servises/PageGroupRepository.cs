using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageGroupRepository : IPagegroupRepository
    {
        //MyCmsContext DB = new MyCmsContext();

        //Dependency Injection
        private MyCmsContext DB;
        public PageGroupRepository(MyCmsContext Context)
        {
            this.DB = Context;
        }

        public IEnumerable<PageGroup> GetAllPagegroup()
        {
            return DB.pagegroup;
        }
        public PageGroup GetGroupByID(int groupID)
        {
            return DB.pagegroup.Find(groupID);
        }

        public bool InsertGroup(PageGroup pagegroup)
        {
            try
            {
                DB.pagegroup.Add(pagegroup);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool UpdateGroup(PageGroup pagegroup)
        {
            try
            {
                DB.Entry(pagegroup).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteGroup(PageGroup pagegroup)
        {
            try
            {
                DB.Entry(pagegroup).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool DeleteGroup(int groupID)
        {
            try
            {
                var Group = GetGroupByID(groupID);
                DeleteGroup(Group);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            DB.SaveChanges();
        }

        public void Dispose()
        {
            DB.Dispose();
        }

        public IEnumerable<ShowGroupViewModel> GetGroupForView()
        {
            return DB.pagegroup.Select(G => new ShowGroupViewModel()
            {
                GroupID = G.GroupID,
                GruopTitle = G.GruopTitle,
                PageCount = G.page.Count
            });
        }
    }
}
