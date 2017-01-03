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
    public class adminSavePromotionsController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: adminSavePromotions
        public ActionResult Index()
        {
            var QueryData = from dataSave in db.tblSavePromotions
                            join dataUser in db.tblUsers on dataSave.IdUserSavePromotion equals dataUser.IdUser
                            join dataPro in db.tblPromotions on dataSave.IdPromotion equals dataPro.IdCode
                            select new tblAll {tblSavePromotion= dataSave,tblPromotion= dataPro,tblUser=dataUser };
            return View(QueryData.ToList());
        }

        // GET: adminSavePromotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSavePromotion tblSavePromotion = db.tblSavePromotions.Find(id);
            if (tblSavePromotion == null)
            {
                return HttpNotFound();
            }
            return View(tblSavePromotion);
        }

        // GET: adminSavePromotions/Create
        public ActionResult Create(tblAll tblall)
        {
            var datauser = (from dataUser in db.tblUsers where dataUser.StatusUser==1 select dataUser).ToList();
            var datapromotion = (from dataPromo in db.tblPromotions where dataPromo.StatusCode == 1 && dataPromo.DateEndCode >= DateTime.Now select dataPromo).ToList();
            tblall.ListUser = datauser;
            tblall.listPromotion = datapromotion;
            return View(tblall);
        }

        // POST: adminSavePromotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public ActionResult Create( tblSavePromotion tblSavePromotion)
        {
            if (ModelState.IsValid)
            {
                tblSavePromotion.StatusSavePromotion = 0;
                db.tblSavePromotions.Add(tblSavePromotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSavePromotion);
        }

        // GET: adminSavePromotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSavePromotion tblSavePromotion = db.tblSavePromotions.Find(id);
            tblAll tp = new tblAll
            {
              tblSavePromotion = db.tblSavePromotions.Find(id),
              ListUser = db.tblUsers.ToList(),
              listPromotion = db.tblPromotions.Where(x=>x.StatusCode==1 && x.DateEndCode >= DateTime.Now).ToList()
            };
            if (tblSavePromotion == null)
            {
                return HttpNotFound();
            }
            return View(tp);
        }

        // POST: adminSavePromotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tblSavePromotion tblSavePromotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSavePromotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSavePromotion);
        }

        // GET: adminSavePromotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSavePromotion tblSavePromotion = db.tblSavePromotions.Find(id);
            if (tblSavePromotion == null)
            {
                return HttpNotFound();
            }
            db.tblSavePromotions.Remove(tblSavePromotion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: adminSavePromotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSavePromotion tblSavePromotion = db.tblSavePromotions.Find(id);
            db.tblSavePromotions.Remove(tblSavePromotion);
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
