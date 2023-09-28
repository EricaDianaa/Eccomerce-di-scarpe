using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace w14_3.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Users = "Admin")]
        // GET: Admin
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}