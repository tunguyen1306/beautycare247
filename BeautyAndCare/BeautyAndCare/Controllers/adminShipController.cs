using BeautyAndCare.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BeautyAndCare.Controllers
{
    public class adminShipController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();
        // GET: adminShip
        public ActionResult Index()
        {

            var Query = from data in db.tblShips
                        select data;
            return View(Query.ToList());
        }

        // GET: adminShip/Details/5
        public ActionResult Details(int id)
        {
            var Query = from data in db.tblShips
                        select data;
            return View(Query.ToList());
        }

        // GET: adminShip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminShip/Create
        [HttpPost]
        public ActionResult Create(tblShip tblship)
        {

            if (ModelState.IsValid)
            {
                db.tblShips.Add(tblship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblship);
        }

        // GET: adminShip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShip tblShip = db.tblShips.Find(id);
            if (tblShip == null)
            {
                return HttpNotFound();
            }
            return View(tblShip);
        }

        // POST: adminShip/Edit/5
        [HttpPost]
        public ActionResult Edit(tblShip tblship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblship);
        }

        // GET: adminShip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblShip tblShip = db.tblShips.Find(id);
            db.tblShips.Remove(tblShip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
