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
    public class adminVocabulariesController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: adminVocabularies
        public ActionResult Index()
        {
            return View(db.tblVocabularies.ToList());
        }

        // GET: adminVocabularies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVocabulary tblVocabulary = db.tblVocabularies.Find(id);
            if (tblVocabulary == null)
            {
                return HttpNotFound();
            }
            return View(tblVocabulary);
        }

        // GET: adminVocabularies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminVocabularies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create( tblVocabulary tblVocabulary)
        {
            if (ModelState.IsValid)
            {
                db.tblVocabularies.Add(tblVocabulary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblVocabulary);
        }

        // GET: adminVocabularies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVocabulary tblVocabulary = db.tblVocabularies.Find(id);
            if (tblVocabulary == null)
            {
                return HttpNotFound();
            }
            return View(tblVocabulary);
        }

        // POST: adminVocabularies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Edit(tblVocabulary tblVocabulary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblVocabulary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblVocabulary);
        }

        // GET: adminVocabularies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblVocabulary tblVocabulary = db.tblVocabularies.Find(id);
            db.tblVocabularies.Remove(tblVocabulary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: adminVocabularies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblVocabulary tblVocabulary = db.tblVocabularies.Find(id);
            db.tblVocabularies.Remove(tblVocabulary);
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
