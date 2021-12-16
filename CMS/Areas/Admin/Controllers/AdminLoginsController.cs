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
    public class AdminLoginsController : Controller
    {
        
        private ILoginRepository loginRepository;
         MyCmsContext DB = new MyCmsContext();
        public AdminLoginsController()
        {
            loginRepository = new LoginRepository(DB);
        }
        // GET: Admin/AdminLogins
        public ActionResult Index()
        {
            return View(loginRepository.GetAll());
        }


        // GET: Admin/AdminLogins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogID,UserName,Email,Password")] AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                loginRepository.InsertAdmin(adminLogin);
                loginRepository.Save();
                return RedirectToAction("Index");
            }

            return View(adminLogin);
        }

        // GET: Admin/AdminLogins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = loginRepository.GetAdminByID(id.Value);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // POST: Admin/AdminLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogID,UserName,Email,Password")] AdminLogin adminLogin)
        {
            if (ModelState.IsValid)
            {
                loginRepository.UpdateAdmin(adminLogin);
                loginRepository.Save();
                return RedirectToAction("Index");
            }
            return View(adminLogin);
        }

        // GET: Admin/AdminLogins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminLogin adminLogin = loginRepository.GetAdminByID(id.Value);
            if (adminLogin == null)
            {
                return HttpNotFound();
            }
            return View(adminLogin);
        }

        // POST: Admin/AdminLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            loginRepository.DeleteAdmin(id);
            loginRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                loginRepository.Dispose();
                DB.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
