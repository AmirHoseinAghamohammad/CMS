using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace CMS.Areas.Admin.Controllers
{
    [Authorize]
    public class PageGroupsController : Controller
    {
        private IPagegroupRepository PageGroupRepository;
        MyCmsContext DB = new MyCmsContext();
        public PageGroupsController()
        {
            PageGroupRepository = new PageGroupRepository(DB);
        }

        // GET: Admin/PageGroups
        public ActionResult Index()
        {
            return View(PageGroupRepository.GetAllPagegroup());
        }



        // GET: Admin/PageGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/PageGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GruopTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                PageGroupRepository.InsertGroup(pageGroup);
                PageGroupRepository.Save();
                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = PageGroupRepository.GetGroupByID(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,GruopTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                PageGroupRepository.UpdateGroup(pageGroup);
                PageGroupRepository.Save();
                return RedirectToAction("Index");
            }
            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PageGroup pageGroup = PageGroupRepository.GetGroupByID(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }
            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PageGroupRepository.DeleteGroup(id);
            PageGroupRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                PageGroupRepository.Dispose();
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
