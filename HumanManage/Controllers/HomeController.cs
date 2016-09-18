using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanManage.Helper;

namespace HumanManage.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            List<string> rsa = RSAHelper.RSAGet();
            ViewData.Add("E", rsa[0]);
            ViewData.Add("M", rsa[1]);
            return View();
        }

       [HttpPost]
        public ActionResult LoginAction(string user_name, string user_pwd)
        {
            //return Json("success");
            return RedirectToAction("Index"); /*同一控制器内跳转*/
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
