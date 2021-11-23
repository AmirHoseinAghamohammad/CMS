using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageRepository : IPageRepository
    {

        //MyCmsContext DB = new MyCmsContext();

        //Dependency Injection
        private MyCmsContext DB;
        public PageRepository(MyCmsContext Context)
        {
            this.DB = Context;
        }
        public IEnumerable<Page> GetAllPage()
        {
            return DB.Page;
        }

        public Page GetPageByID(int pageid)
        {
            return DB.Page.Find(pageid);
        }

        public bool InsertPage(Page page)
        {
            try
            {
                DB.Page.Add(page);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool UpdatePage(Page page)
        {
            try
            {
                DB.Entry(page).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeletePage(Page page)
        {
            try
            {
                DB.Entry(page).State = EntityState.Deleted;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletePage(int pageid)
        {
            try
            {
                var page = GetPageByID(pageid);
                DeletePage(page);
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

        public IEnumerable<Page> TopNews(int take = 4)
        {
            return DB.Page.OrderByDescending(p => p.Visit).Take(take);
        }

        public IEnumerable<Page> PagesInSlider()
        {
            return DB.Page.Where(p => p.ShowInSlider == true);
        }

        public IEnumerable<Page> LastNews(int take = 4)
        {
            return DB.Page.OrderByDescending(p => p.CreateDate).Take(take);
        }

        public IEnumerable<Page> ShowPageByGroupID(int Groupid)
        {
            return DB.Page.Where(p => p.GroupID == Groupid);
        }

        public IEnumerable<Page> ShearchPage(string Search)
        {
            return DB.Page.Where(p => p.Title.Contains(Search) || p.ShortDiscription.Contains(Search) ||
            p.Tags.Contains(Search) || p.Text.Contains(Search)).Distinct();
        }
    }
}
