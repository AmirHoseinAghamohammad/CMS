using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Controllers
{
    public class NewsController : Controller
    {
        MyCmsContext DB = new MyCmsContext();
        private IPagegroupRepository PageGroupRepository;
       private IPageRepository PageRepository;
        private IPageCommentRepository PageCommentRepository;
        public NewsController()
        {
            PageGroupRepository = new PageGroupRepository(DB);
            PageRepository = new PageRepository(DB);
            PageCommentRepository = new PageCommentRepository(DB);
        }



        // GET: News
        [ChildActionOnly]
        public ActionResult ShowGroup()
        {
            return PartialView(PageGroupRepository.GetGroupForView());
        }


        [ChildActionOnly]
        public ActionResult ShowGroupInMenu()
        {
            return PartialView(PageGroupRepository.GetAllPagegroup());
        }


        [ChildActionOnly]
        public ActionResult TopNews()
        {
            return PartialView(PageRepository.TopNews());
        }

        [ChildActionOnly]

        public ActionResult LateNews()
        {
            return PartialView(PageRepository.LastNews()); 
        }

        [Route("Archive")]
        public ActionResult ArchiveNews()
        {
            return View(PageRepository.GetAllPage());
        }

        [Route("Group/{id}/{Title}")]
        public ActionResult ShowNewsByGroupID(int id, string title)
        {
            ViewBag.name = title;
            return View(PageRepository.ShowPageByGroupID(id));
        }

        [Route("News/{id}")]
       public  ActionResult ShowNews (int id)
        {
            var News = PageRepository.GetPageByID(id);
            if (News == null)
            {
                return HttpNotFound();
            }
            News.Visit += 1;
            PageRepository.UpdatePage(News);
            PageRepository.Save();
            return View(News);
        }

        [ChildActionOnly]
        public ActionResult AddComment(int id,string name,string email,string Comment)
        {
            PageComment AddComment = new PageComment()
            {
                CreateDateComment = DateTime.Now,
                PageID = id,
                Comment=Comment,
                Email = email,
                Name = name
            };
            PageCommentRepository.AddComment(AddComment);

            return PartialView("ShowComment",PageCommentRepository.GetCommentByNewsID(id));
        }

        [ChildActionOnly]

        public ActionResult ShowComment(int id)
        {
            return PartialView(PageCommentRepository.GetCommentByNewsID(id));
        }
    }
}