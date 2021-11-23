using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Controllers
{
    public class SearchController : Controller
    {
        private IPageRepository PageRepository;
        MyCmsContext DB = new MyCmsContext();
        public SearchController()
        {
            PageRepository = new PageRepository(DB);
        }

        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.shearch = q;
            return View(PageRepository.ShearchPage(q));
        }
    }
}