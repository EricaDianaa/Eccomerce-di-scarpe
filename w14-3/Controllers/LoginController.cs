using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using w14_3.Models;

namespace w14_3.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            FormsAuthentication.SetAuthCookie(u.Username, false);
            return RedirectToAction("Index", "Home");
        }
    }
}