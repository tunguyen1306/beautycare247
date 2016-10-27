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
using ImageResizer;
using System.Configuration;

namespace BeautyAndCare.Controllers
{
    public class adminProductController : Controller
    {
        private WebSiteEntities db = new WebSiteEntities();

        // GET: adminProduct
        public ActionResult Index()
        {
            var qrData = (from dataPro in db.tblProducts
                          join dataImg in db.tblPictures on dataPro.IdProducts equals dataImg.ProductsId
                          join dataMenu in db.tblMenus on dataPro.IdCategoryProducts equals dataMenu.IdMenu
                          join dataType in db.tblTypes on dataPro.IdTypeProducts equals dataType.IdType
                          where dataImg.Position == 1
                          select new tblAll { tblProducts = dataPro, clPicture = dataImg,tblMenu= dataMenu, tblType=dataType });
            return View(qrData.ToList());
        }

        // GET: adminProduct/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProduct tblProduct = db.tblProducts.Find(id);
            if (tblProduct == null)
            {
                return HttpNotFound();
            }
            return View(tblProduct);
        }

        // GET: adminProduct/Create
        public ActionResult Create(tblAll _tblproduct)
        {
            if (ModelState.IsValid)
            {
                db.tblProducts.Add(_tblproduct.tblProducts);
                db.SaveChanges();


            }
            var dataMenu = (from data in db.tblMenus
                            where data.IsCategoryMenu==1
                               select  data).ToList();
            var dataType = (from data in db.tblTypes
                                select data).ToList();
            _tblproduct.ListMenu = dataMenu;
            _tblproduct.ListType = dataType;
           
            return View(_tblproduct);
            
        }
        [HttpPost]
        public void SaveImg(tblAll productPicture)
        {
            var t = productPicture.cfile == null ? "" : productPicture.cfile;
            var file = t.Replace("data:image/png;base64,", "");
            var photoBytes = Convert.FromBase64String(file);
            string format = "jpg";
            if (productPicture.isactive == 1)
            {
                productPicture.isactive = 1;
            }
            else
            {
                productPicture.isactive = 2;
            }
            var picture = new tblAll
            {
                clPicture = new tblPicture { ProductsId = productPicture.idProducts, AngleType = 0, Converted = DateTime.Now, ToCheck = true, Type_id = 1, Title = productPicture.nameImg, Position = productPicture.isactive }
            };
            if (productPicture.idpicture == 0)
            {
                var settings = new ResizeSettings();
                settings.Scale = ScaleMode.DownscaleOnly;
                settings.Format = format;

                //string uploadFolder = picture.DirectoryPhysical;

                string path = Server.MapPath("~/fileUpload/") + DateTime.Now.Day + DateTime.Now.Month + "/";
                // check for directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                // filename with placeholder for size
                if (picture.GetConvertedFileName() == null || string.IsNullOrWhiteSpace(picture.GetConvertedFileName()))
                    picture.SetFileName(DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + "_" + picture.CreateFilename() + "_{0}." + format);

                if (picture.GetFilePathPhysical(tblAll.PictureSize.Large) != null)
                {
                    string dest = path + picture.FileName(tblAll.PictureSize.Large);
                    settings.MaxWidth = 800;
                    settings.MaxHeight = 800;
                    if (picture.WaterMarkLarge == tblAll.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.clPicture.OriginalFilepath))
                        picture.clPicture.OriginalFilepath = picture.GetFilePath(tblAll.PictureSize.Large);
                }

                if (picture.GetFilePathPhysical(tblAll.PictureSize.Medium) != null)
                {
                    string dest = path + picture.FileName(tblAll.PictureSize.Medium);
                    settings.MaxWidth = 300;
                    settings.MaxHeight = 300;
                    if (picture.WaterMarkLarge == tblAll.WatermarkType.None)
                        ImageBuilder.Current.Build(photoBytes, dest, settings, false, false);
                    // save biggest version as original
                    if (string.IsNullOrWhiteSpace(picture.clPicture.OriginalFilepath))
                        picture.clPicture.OriginalFilepath = picture.GetFilePath(tblAll.PictureSize.Medium);
                }


                db.tblPictures.Add(picture.clPicture);
                db.SaveChanges();
            }
            if (productPicture.idpicture > 0)
            {

                tblPicture tblpict = db.tblPictures.Find(productPicture.idpicture);
                tblpict.Title = productPicture.nameImg;
                tblpict.Position = productPicture.isactive;
                db.Entry(tblpict).State = EntityState.Modified;
                db.SaveChanges();
                RedirectToAction("Index", "adminProducts");
            }


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPro(tblAll productPicture)
        {
            tblProduct tblproduct = productPicture.tblProducts;

            db.Entry(tblproduct).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "adminProduct");
        }
        [HttpPost]
        public ActionResult DeleteImg(int idpicture)
        {
            tblPicture tblPic = db.tblPictures.Find(idpicture);
            tblAll proPic = new tblAll();
            db.tblPictures.Remove(tblPic);
            db.SaveChanges();
            DeleteIMG(proPic.clPicture.OriginalFilepath);
            return View(tblPic);
        }
        public void DeleteIMG(string picture)
        {
            tblAll vgp = new tblAll();
            if (picture == null)
                return;
            var fo = picture.Substring(0, 3);
            string dir = Server.MapPath("~/fileUpload/" + fo + "/" + picture);

            System.IO.File.Delete(dir);



        }
        [HttpPost]
        public ActionResult Cancel(int id)
        {
            tblProduct tblproduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblproduct);
            db.SaveChanges();
            return RedirectToAction("Index", "adminProduct");



        }
        // GET: adminProduct/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAll pic = new tblAll
            {
                tblProducts = db.tblProducts.Find(id), classPicture = db.tblPictures.Where(t => t.ProductsId == id).ToList(),
                ListCategory=db.tblCategories.ToList(),
                ListType = db.tblTypes.ToList(),
                 ListMenu = db.tblMenus.Where(x=>x.IsCategoryMenu==1).ToList()
            };
            return View(pic);
        }

        // POST: adminProduct/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( tblAll tblall)
        {
            if (ModelState.IsValid)
            {

                db.Entry(tblall.tblProducts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblall);
        }

        // GET: adminProduct/Delete/5
        public ActionResult Delete(int? id)
        {
            tblProduct tblpro = db.tblProducts.Find(id);
            List<tblPicture> list = (from t in db.tblPictures where t.ProductsId == id select t).ToList();
            foreach (tblPicture item in list)
            {
                db.tblPictures.Remove(item);
            }
            db.SaveChanges();
            db.tblProducts.Remove(tblpro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: adminProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProduct tblProduct = db.tblProducts.Find(id);
            db.tblProducts.Remove(tblProduct);
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
