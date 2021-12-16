using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.IO;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
       private IPageRepository PageRepository;
        private IPagegroupRepository PageGroupRepository;

        private MyCmsContext DB = new MyCmsContext();
        public PagesController()
        {
           PageRepository = new PageRepository(DB);
            PageGroupRepository = new PageGroupRepository(DB);
        }
        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(PageRepository.GetAllPage());
        }

        // GET: Admin/Pages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = DB.Page.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(PageGroupRepository.GetAllPagegroup(), "GroupID", "GruopTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Title,ShortDiscription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page,HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                page.Visit = 0;
                page.CreateDate = DateTime.Now;

                if (ImgUp != null)
                {
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/PageImages/"+page.ImageName));
                }
                PageRepository.InsertPage(page);
                PageRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(DB.pagegroup, "GroupID", "GruopTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = PageRepository.GetPageByID(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(PageGroupRepository.GetAllPagegroup(), "GroupID", "GruopTitle", page.GroupID);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Title,ShortDiscription,Text,Visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page,HttpPostedFileBase ImgUp)
        {
            if (ModelState.IsValid)
            {
                if (ImgUp!=null)
                {
                    if (page.ImageName!=null)
                    {
                        System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
                    }

                    page.ImageName = Guid.NewGuid() + Path.GetExtension(ImgUp.FileName);
                    ImgUp.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));

                }
                PageRepository.UpdatePage(page);
                PageRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(PageGroupRepository.GetAllPagegroup(), "GroupID", "GruopTitle", page.GroupID);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = PageRepository.GetPageByID(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = PageRepository.GetPageByID(id);
            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
            }

            PageRepository.DeletePage(page);
            PageRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PageGroupRepository.Dispose();
                PageRepository.Dispose();

                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
