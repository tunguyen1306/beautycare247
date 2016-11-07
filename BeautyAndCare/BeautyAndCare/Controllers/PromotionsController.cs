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
    public class PromotionsController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: Promotions
        public ActionResult Index()
        {
            return View(db.tblPromotions.ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPromotion tblPromotion = db.tblPromotions.Find(id);
            if (tblPromotion == null)
            {
                return HttpNotFound();
            }
            return View(tblPromotion);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
   
        public ActionResult Create( tblPromotion tblPromotion)
        {
            if (ModelState.IsValid)
            {
                db.tblPromotions.Add(tblPromotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblPromotion);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPromotion tblPromotion = db.tblPromotions.Find(id);
            if (tblPromotion == null)
            {
                return HttpNotFound();
            }
            return View(tblPromotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Edit( tblPromotion tblPromotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblPromotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblPromotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblPromotion tblPromotion = db.tblPromotions.Find(id);
            db.tblPromotions.Remove(tblPromotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPromotion tblPromotion = db.tblPromotions.Find(id);
            db.tblPromotions.Remove(tblPromotion);
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
