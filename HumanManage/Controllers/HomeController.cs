using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanManage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

       [HttpPost]
        public ActionResult LoginAction(string user_login, string user_password)
        {
            //if (string.IsNullOrEmpty(login))
            //    return RedirectToAction("Login");
            //if (login.Equals("LoginAction"))
                return RedirectToAction("Index"); /*同一控制器内跳转*/
            //return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
