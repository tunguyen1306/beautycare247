using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyAndCare.Models;

namespace BeautyAndCare.Controllers
{
    public class adminMenuController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: adminMenu
        public ActionResult Index()
        {
            var dataMenu1 = from data in db.tblMenus where data.StatusMenu==1 select data ;
            var dataMenu = from data in db.tblMenus
                           where data.StatusMenu == 1
                           select new tblMenuDto
                           {
                               tblMenu = data,
                               NameMenuParent= dataMenu1.Where(x=>x.IdMenu== data.IdParentMenu).Select(t=>t.NameMenu).FirstOrDefault()

                           };
            return View(dataMenu.ToList());
        }

        // GET: adminMenu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMenu tblMenu = db.tblMenus.Find(id);
            if (tblMenu == null)
            {
                return HttpNotFound();
            }
            return View(tblMenu);
        }

        // GET: adminMenu/Create
        public ActionResult Create()
        {
            var tbl = new tblMenuDto();
            var dataMenu = (from data in db.tblMenus
                           where data.StatusMenu==1
                           select data).ToList();
            tbl.ListMenu = dataMenu;
            return View(tbl);
        }

        // POST: adminMenu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( tblMenuDto _tblMenu)
        {
            if (ModelState.IsValid)
            {
                db.tblMenus.Add(_tblMenu.tblMenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: adminMenu/Edit/5
        public ActionResult Edit(int? id)
        {
            var tbl = new tblMenuDto();
            var dataMenu = (from data in db.tblMenus
                            where data.StatusMenu == 1 && data.IdMenu==id
                            select data);
            var dataListMenu = (from data in db.tblMenus
                            where data.StatusMenu == 1 
                            select data);
            tbl.tblMenu = db.tblMenus.Find(id);
            tbl.ListMenu = dataListMenu.ToList();
            return View(tbl);
        }

        // POST: adminMenu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Edit( tblMenuDto _tblMenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(_tblMenu.tblMenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(_tblMenu);
        }

        // GET: adminMenu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblMenu tblMenu = db.tblMenus.Find(id);
            if (tblMenu == null)
            {
                return HttpNotFound();
            }
            return View(tblMenu);
        }

        // POST: adminMenu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMenu tblMenu = db.tblMenus.Find(id);
            db.tblMenus.Remove(tblMenu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
