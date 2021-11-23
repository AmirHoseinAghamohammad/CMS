using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer;
using System.Web.Security;

namespace CMS.Controllers
{
    public class AccountController : Controller
    {
        MyCmsContext DB = new MyCmsContext();
        private ILoginRepository LoginRepository;
        public AccountController()
        {
            LoginRepository = new LoginRepository(DB);
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel Login, string ReturnUrl="/")
        {
            if (ModelState.IsValid)
            {
                if (LoginRepository.IsExistUser(Login.UserName,Login.Password))
                {
                    FormsAuthentication.SetAuthCookie(Login.UserName, Login.RemmeberMe);
                    return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError("UserName", "کاربر مورد نظر یافت نشد");
                }
            }
            return View(Login);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
    }
}