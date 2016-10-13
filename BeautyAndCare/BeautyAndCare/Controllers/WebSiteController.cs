using BeautyAndCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BeautyAndCare.Controllers
{
    public class WebSiteController : Controller
    {
        WebSiteEntities db = new WebSiteEntities();
        // GET: WebSite
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Header()
        {
            var queryMenu = from data in db.tblMenus
                            where data.StatusMenu == 1
                            select  data;
            return PartialView(queryMenu.ToList());
        }
        public ActionResult Footer()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(tblUser tbluser)
        {
            tbluser.IdTypeUser = 3;
            tbluser.TotalPointUser = 0;
            tbluser.SubscribeUser = true;
            db.tblUsers.Add(tbluser);
            db.SaveChanges();
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Breadcrumb()
        {
            return View();
        }
        public ActionResult Detail()
        {
            return View();
        }
    }
}