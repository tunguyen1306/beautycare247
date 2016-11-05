using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BeautyAndCare.Models;
using System.IO;
using System.Configuration;

namespace BeautyAndCare.Controllers
{
    public class adminSlidersController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: adminSliders
        public ActionResult Index()
        {
            return View(db.tblSliders.ToList());
        }

        // GET: adminSliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSlider tblSlider = db.tblSliders.Find(id);
            if (tblSlider == null)
            {
                return HttpNotFound();
            }
            return View(tblSlider);
        }

        // GET: adminSliders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: adminSliders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create(tblSlider tblSlider)
        {
            if (ModelState.IsValid)
            {
                db.tblSliders.Add(tblSlider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSlider);
        }

        // GET: adminSliders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSlider tblSlider = db.tblSliders.Find(id);
            if (tblSlider == null)
            {
                return HttpNotFound();
            }
            return View(tblSlider);
        }

        // POST: adminSliders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit(tblSlider tblSlider)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSlider).State = EntityState.Modified;
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblSlider);
        }

        // GET: adminSliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSlider tblSlider = db.tblSliders.Find(id);
            if (tblSlider == null)
            {
                return HttpNotFound();
            }
           
            db.tblSliders.Remove(tblSlider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: adminSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSlider tblSlider = db.tblSliders.Find(id);
            db.tblSliders.Remove(tblSlider);
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
        [HttpPost, ActionName("GetLink")]
        public JsonResult GetLink()
        {
            var path = string.Empty; var path1 = string.Empty;
            var NewPath = string.Empty;
            var fortmatName = string.Empty;
            var fileNameFull = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var file = System.Web.HttpContext.Current.Request.Files["HelpSectionImages"];
                if (file != null && file.ContentLength > 0)
                {

                    var fileName = Path.GetFileName(file.FileName);
                    string newFileNmae = Path.GetFileNameWithoutExtension(fileName);
                    fortmatName = Path.GetExtension(fileName);

                    NewPath = newFileNmae.Replace(newFileNmae, (DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString()).ToString());
                    fileNameFull = DateTime.Now.Day + "" + DateTime.Now.Month + "_" + NewPath + fortmatName;
                    path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    path1 = Path.Combine(path, fileNameFull);

                    file.SaveAs(path1);
                }
            }

            if (HttpContext.Request.Url != null && HttpContext.Request.Url.Host.Contains("localhost"))
            {
                path = HttpContext.Request.Url.Host + ":" + HttpContext.Request.Url.Port + "/fileUpload/" + DateTime.Now.Day + DateTime.Now.Month + "/";
            }
            else
            {
                path = ConfigurationManager.AppSettings["domain"] + DateTime.Now.Day + DateTime.Now.Month + "/";
            }
            var _fullUrl = path + fileNameFull;
            return Json(new
            {
                fullurl = _fullUrl,
                shorurl = "/fileUpload/" + DateTime.Now.Day + DateTime.Now.Month + "/" + fileNameFull,
                imgName = fileNameFull
            });
        }
    }
}
