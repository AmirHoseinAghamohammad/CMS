using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataLayer
{
    public class PageCommentRepository : IPageCommentRepository
    {

       private MyCmsContext DB ;

        public PageCommentRepository(MyCmsContext Context)
        {
            DB = Context;
        }


        public IEnumerable<PageComment> GetCommentByNewsID(int Pageid)
        {
            return DB.pagecomment.Where(P => P.PageID == Pageid);
        }

        public bool AddComment(PageComment Comment)
        {
            try
            {
                DB.pagecomment.Add(Comment);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
