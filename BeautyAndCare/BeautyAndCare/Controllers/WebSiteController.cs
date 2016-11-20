using BeautyAndCare.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.Data.Entity;

namespace BeautyAndCare.Controllers
{
    public class WebSiteController : Controller
    {
        WebSiteEntities db = new WebSiteEntities();
        // GET: WebSite
        public ActionResult Index()
        {
            var tbl = new tblAll();
            var tbllist = new ListAll();
            var queryListMenu = (from data in db.tblMenus
                                 where data.StatusMenu == 1 && data.ShowHomeMenu == 1
                                 select data).ToList();
            var dataNewProducts = from dataProducts in db.tblProducts
                                  join dataCategory in db.tblMenus on dataProducts.IdCategoryProducts equals dataCategory.IdMenu
                                  where dataProducts.StatusProducts == 1
                                  orderby dataProducts.IdProducts descending
                                  select dataProducts;
            var queryListPicture = (from data in db.tblPictures
                                        // where data.Position==1
                                    select data).ToList();
            var querySlider = (from dataSlider in db.tblSliders
                               where dataSlider.StatusSlider == 1
                               select dataSlider).ToList();
            var queryBlogNo = (from dataBlog in db.tblBlogs
                               where dataBlog.StatusBlog == 1 && dataBlog.IsVideo == 0
                               orderby dataBlog.IdBlog descending
                               select dataBlog).Take(2).ToList();
            var queryBlogIs = (from dataBlog in db.tblBlogs
                               where dataBlog.StatusBlog == 1 && dataBlog.IsVideo == 1
                               orderby dataBlog.IdBlog descending
                               select dataBlog).Take(1).ToList();
            tbl.ListMenu = queryListMenu;
            tbl.ListPicture = queryListPicture;
            tbl.tblPro = dataNewProducts.ToList();
            tbl.ListSlider = querySlider;
            tbl.ListBlogNo = queryBlogNo;
            tbl.ListBlogIs = queryBlogIs;
            return View(tbl);
        }
        public ActionResult Header()
        {
            var queryMenu = from data in db.tblMenus
                            where data.StatusMenu == 1
                            select data;
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
        public ActionResult Login(string email, string pass)
        {
            if (email == "admin@gmail.com" & pass == "admin123trang")
            {
                Session["EmailUser"] ="admin123";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["IdUser"] = null;
                Session["EmailUser"] = null;
                Session["IdTypeUser"] = null;
                Session["FullName"] = null;
                var data = db.tblUsers.ToList().Where(x => x.EmailUser == email && x.PassUser == pass)
                    .Select(x =>
                    new
                    {
                        x.FirtNameUser,
                        x.LastNameUser,
                        x.IdUser,
                        x.EmailUser,
                        x.IdTypeUser,
                        x.NameUser,
                        x.PhoneUser,
                        x.AddressUser
                    })
                    .FirstOrDefault();
                if (data != null)
                {
                    FormsAuthentication.SetAuthCookie(data.EmailUser, false);
                    String returnUrl = Request.Params["ReturnUrl"];


                    Session["IdUser"] = data.IdUser;
                    Session["EmailUser"] = data.EmailUser;
                    Session["IdTypeUser"] = data.IdTypeUser;
                    Session["FullName"] = data.LastNameUser + data.FirtNameUser;
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("Index", "WebSite");
                    }
                    else
                    {
                        return Redirect(Server.UrlDecode(returnUrl));
                    }

                }

            }

            return View();
        }
        [HttpPost]
        public ActionResult Register(tblUser tbluser)
        {
            var vaEmail = db.tblUsers.Where(x => x.EmailUser == tbluser.EmailUser).Count();

            if (vaEmail == 0)
            {
                tbluser.TypeRegister = 1;
                tbluser.IdTypeUser = 3;
                tbluser.TotalPointUser = 0;
                tbluser.SubscribeUser = 1;
                tbluser.IdRoleUser = 1;
                db.tblUsers.Add(tbluser);
                db.SaveChanges();
                return RedirectToAction("Login", "WebSite");
            }
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
        public ActionResult Detail(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());

            tblAll pic = new tblAll { tblProductRel = db.tblProducts.ToList(), tblPro = db.tblProducts.Where(t => t.IdProducts == id_).ToList(), ListPicture = db.tblPictures.Where(t => t.ProductsId == id_).ToList() };
            return View(pic);
        }
        public ActionResult CheckOut()
        {
            if (Session["IdUser"] == null)
            {
                return RedirectToAction("Login", "WebSite");
            }

            ShoppingCartModel model = new ShoppingCartModel();
            model.Cart = (ShoppingCart)Session["Cart"];
            return View(model);
        }
        [HttpPost]
        public ActionResult AddToCart(int ProductId)
        {
            var dataPro = db.tblProducts.Single(x => x.IdProducts == ProductId);
            if (dataPro != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                var Urlimg = ConfigurationManager.AppSettings["domain"];
                ShoppingCartItem cartIem = new ShoppingCartItem()
                {
                    ProductName = dataPro.NameProducts,
                    ProductID = dataPro.IdProducts,
                    Price = dataPro.PriceNewProducts,
                    Quanlity = 1,
                    Total = Convert.ToDecimal(dataPro.PriceNewProducts),
                    SubTotal = Convert.ToDecimal(dataPro.PriceNewProducts),
                    NameImg = db.tblPictures.Where(x => x.ProductsId == dataPro.IdProducts && x.Position == 1).FirstOrDefault().ConvertedFilename,
                    UrlImg = Urlimg,
                    FullUrlImg2 = Urlimg + String.Format(db.tblPictures.Where(x => x.ProductsId == dataPro.IdProducts && x.Position == 1).FirstOrDefault().ConvertedFilename, 2).ToString().Split('_')[0] + "/" + String.Format(db.tblPictures.Where(x => x.ProductsId == dataPro.IdProducts && x.Position == 1).FirstOrDefault().ConvertedFilename, 2)

                };
                cart.AddToCart(cartIem);
                Session["cart"] = cart;

                var dataProCount = cart.ListItem.Count();
                var nameImg = cart.ListItem.Select(x => x.NameImg).FirstOrDefault();
                var fullUrlImg2 = Urlimg + String.Format(nameImg, 2).ToString().Split('_')[0] + "/" + String.Format(nameImg, 2);
                return Json(new { dataPro = dataPro, count = dataProCount, Urlimg = Urlimg, nameImg = fullUrlImg2 });
                //return Json(dataPro);

            }
            return Json(dataPro);
        }
        public ActionResult UpdateToCart(int ProductId, int Quantity)
        {

            ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart != null)
            {
                cart.UpdateCart(ProductId, Quantity);
                Session["cart"] = cart;
            }
            //ShoppingCartModel model = new ShoppingCartModel();
            //model.Cart = (ShoppingCart)Session["Cart"];
            return RedirectToAction("CheckOut");


        }
        public ActionResult CheckCoupon(string coupon)
        {
            var dataCoupon = from data in db.tblPromotions
                             where data.NameCode == coupon
                             select data;
            return Json(dataCoupon.ToList());
        }
        //public ActionResult UpdateToCartCoupon(int showSubTotal,int showSubTotalAll,int index)
        //{

        //    return Json();
        //}
        public ActionResult ListProducts(string id, int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 10;
            var id_ = int.Parse(id.Split('-').Last());
            ViewBag.TypeID = id_;
            var dataMe = (from datame in db.tblMenus
                          where datame.IdMenu == id_
                          select datame).ToList();
            var data = from dataPro in db.tblProducts
                       join dataPic in db.tblPictures on dataPro.IdProducts equals dataPic.ProductsId
                       join dataMenu in db.tblMenus on dataPro.IdCategoryProducts equals dataMenu.IdMenu
                       where dataPic.Position == 1 && dataPro.IdCategoryProducts == id_
                       select new tblAll { tblProducts = dataPro, clPicture = dataPic, tblMenu = dataMenu };

            return View(data.ToList().ToPagedList(pagenum, pageSize));

        }
        public ActionResult ListProducts1(string id, int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 10;
            var id_ = int.Parse(id.Split('-').Last());
            ViewBag.TypeID = id_;
            var dataMe = (from datame in db.tblMenus
                          where datame.IdMenu == id_
                          select datame).ToList();
            var data = from dataPro in db.tblProducts
                       join dataPic in db.tblPictures on dataPro.IdProducts equals dataPic.ProductsId
                       join dataMenu in db.tblMenus on dataPro.IdCategoryProducts equals dataMenu.IdMenu
                       where dataPic.Position == 1 && dataPro.IdCategoryProducts == id_
                       select new tblAll { tblProducts = dataPro, clPicture = dataPic, tblMenu = dataMenu };

            return PartialView("ListProducts", data.ToList().ToPagedList(pagenum, pageSize));

        }
        public ActionResult ListProductsById()
        {

            return View();

        }
        public ActionResult PayOut()
        {

            return View();

        }
        public ActionResult Slider()
        {
            var querySlider = (from dataSlider in db.tblSliders
                               where dataSlider.StatusSlider == 1
                               select dataSlider).ToList();
            return View(querySlider);

        }
        public ActionResult Blogs()
        {
            var queryBlog = (from dataBlog in db.tblBlogs
                             where dataBlog.StatusBlog == 1
                             select dataBlog).ToList();
            return View(queryBlog);

        }
        public ActionResult DetailBlog(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            var queryBlog = (from dataBlog in db.tblBlogs
                             where dataBlog.IdBlog == id_
                             select dataBlog).ToList();
            return View(queryBlog);

        }
        [HttpPost]
        public ActionResult GetRowLan(int lan)
        {
            var dataVo = from data in db.tblVocabularies
                         where data.IdLangVocabulary == lan
                         select data;


            return Json(dataVo.ToList());
        }
        public ActionResult About()
        {

            return View();

        }
        [HttpPost]
        public ActionResult sendContact(string name, string email, string enquiry)
        {

            var t = email;
            var mail = new MailMessage();
            mail.To.Add("BeautyandCare247@gmail.com");
            mail.From = new MailAddress("BeautyandCare247@gmail.com");
            mail.Subject = "Email khách hàng đăng ký nhận tin";
            mail.Body = "Email của khách là:" + email;
            mail.IsBodyHtml = true;
            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential
                ("BeautyandCare247@gmail.com", "trang0611");
            smtp.EnableSsl = true;
            smtp.Send(mail);

            return RedirectToAction("Index");

        }
        public ActionResult ListBlog(int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 10;
            var queryList = (from data in db.tblBlogs
                             where data.StatusBlog == 1
                             select data).ToList();

            return View(queryList.ToPagedList(pagenum, pageSize));

        }
        public ActionResult ListBlogBy(int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 10;
            var queryList = (from data in db.tblBlogs
                             where data.StatusBlog == 1
                             select data).ToList();

            return PartialView("ListBlog", queryList.ToList().ToPagedList(pagenum, pageSize));

        }
        public ActionResult MyAccount(int userId)
        {
            var queryProduct = (from dataPro in db.tblProducts
                                join dataPic in db.tblPictures on dataPro.IdProducts equals dataPic.ProductsId
                                select dataPro).ToList();
            var queryHistory = (from dataHis in db.tblHistories
                                where dataHis.IdUser == userId
                                select dataHis).ToList();

            var querydata = (from dataUser in db.tblUsers

                             where dataUser.IdUser == userId
                             select new tblUserDto
                             {
                                 tblUser = dataUser,
                                 ListHistory = db.tblHistories.Where(x=>x.IdUser== userId).ToList(),
                                 IdUser = userId
                             }).ToList();



            return View(querydata);

        }
        public ActionResult UpdateAccount(int userid, string email, string fname, string lname, int phone)
        {
            tblUser tblBlog = db.tblUsers.Find(userid);
            tblBlog.EmailUser = email;
            tblBlog.FirtNameUser = fname;
            tblBlog.LastNameUser = lname;
            tblBlog.PhoneUser = phone;
            if (ModelState.IsValid)
            {
                db.Entry(tblBlog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();

        }
        [HttpPost]
        public ActionResult GetCity()
        {
            var dataCity = from data in db.States
                           join datatext in db.StateTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi"
                           select new GeoDto { CityId = data.state_id, CityName = datatext.text };
            return Json(dataCity.ToList());
        }
        [HttpPost]
        public ActionResult GetDistrict(int id)
        {
            var dataDistrict = from data in db.Districts
                               join datatext in db.DistrictTexts on data.name_id equals datatext.id
                               where datatext.language_id == "vi" && data.state_id == id
                               select new GeoDto { DistId = data.district_id, DistName = datatext.text };
            return Json(dataDistrict.ToList());
        }
        [HttpPost]
        public ActionResult GetWard(int id)
        {
            var dataWard = from data in db.Locations
                           join datatext in db.LocationTexts on data.name_id equals datatext.id
                           where datatext.language_id == "vi" && data.district_id == id
                           select new GeoDto { WardId = data.location_id, WardName = datatext.text };
            return Json(dataWard.ToList());
        }
        [HttpPost]
        public ActionResult GetPriceShip(int id)
        {
            var dataWard = from data in db.tblShips
                           where data.IdCityShip == id
                           select data;
            return Json(dataWard.ToList());
        }

        [HttpPost]
        public ActionResult SendOrder(string content)
        {
            var t = 0;
            try
            {
                var mail = new MailMessage();
                mail.To.Add("BeautyandCare247@gmail.com");
                mail.From = new MailAddress("BeautyandCare247@gmail.com");
                mail.Subject = "Email khách hàng mua hàng";
                mail.Body = content;
                mail.IsBodyHtml = true;
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential
                    ("BeautyandCare247@gmail.com", "trang0611");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                t = 0;
                Session.Remove("Cart");
                Session["Cart"] = null;
            }
            catch (Exception)
            {
                t = 1;
                throw;
            }

            return Json(t);
        }
        [HttpPost]
        public ActionResult AddPoint(int point)
        {
            var userId = int.Parse(Session["IdUser"].ToString());
            var getPoint = db.tblUsers.Find(userId).TotalPointUser;
            var tblPoint = db.tblUsers.Find(userId);
            tblPoint.TotalPointUser = getPoint + point;
            tblPoint.IdUser = userId;
            db.Entry(tblPoint).State = EntityState.Modified;
            db.SaveChanges();
            return Json(tblPoint);
        }
        [HttpPost]
        public ActionResult AddProduct(int idOrder, int idPro)
        {
            var tblorderdetail = new tblOrderDetail();
            tblorderdetail.IdOrder = idOrder;
            tblorderdetail.IdProducts = idPro;
            tblorderdetail.DateCreate = DateTime.Now;
            db.tblOrderDetails.Add(tblorderdetail);
            db.SaveChanges();
            return Json(tblorderdetail);
        }
        [HttpPost]
        public ActionResult AddOrder(int priceTotal)
        {
            var tblorder = new tblOrder();
            tblorder.IdUser = int.Parse(Session["IdUser"].ToString());
            tblorder.DateCreate = DateTime.Now;
            tblorder.PriceTotal = priceTotal.ToString();
            tblorder.PointTotal = priceTotal / 10000;
            db.tblOrders.Add(tblorder);
            db.SaveChanges();
            return Json(tblorder);
        }

        public ActionResult LogOut()
        {
            Session["IdUser"] = null;
            Session["EmailUser"] = null;
            Session["IdTypeUser"] = null;
            Session["FullName"] = null;
            Session.Remove("Cart");
            Session.Remove("IdUser");
            Session.Remove("EmailUser");
            Session.Remove("IdTypeUser");
            Session.Remove("FullName");
            Session["Cart"] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ShowToCart()
        {
            if (Session["Cart"] != null)
            {
                var Urlimg = ConfigurationManager.AppSettings["domain"];
                ShoppingCartModel model = new ShoppingCartModel();
                model.Cart = (ShoppingCart)Session["Cart"];

                var dataProCount = model.Cart.ListItem.Count();
                var nameImg = model.Cart.ListItem.Select(x => x.NameImg).FirstOrDefault();
                var fullUrlImg2 = Urlimg + String.Format(nameImg, 2).ToString().Split('_')[0] + "/" + String.Format(nameImg, 2);
                return Json(new { dataPro = model.Cart.ListItem, count = dataProCount, Urlimg = Urlimg, nameImg = fullUrlImg2 });
            }
            return View();
        }
        [HttpPost]
        public ActionResult RegisterByFace(string UserId, string name)
        {
            var checkUserId = db.tblUsers.Where(x => x.IdRegisterFB == UserId).Count();
            if (checkUserId==0)
            {
                var tblUser = new tblUser();
                tblUser.TypeRegister = 2;
                tblUser.IdRegisterFB = UserId;
                tblUser.NameUser = name;
                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                Session["IdUser"] = tblUser.IdUser;
                Session["EmailUser"] = name;
                Session["FullName"] = name;
            }
            else
            {
                var GetId = db.tblUsers.Where(x => x.IdRegisterFB == UserId).Select(x=>x.IdUser).FirstOrDefault();


                var tblUser = db.tblUsers.Find(GetId);
                tblUser.TypeRegister = 2;
                tblUser.IdRegisterFB = UserId;
                tblUser.NameUser = name;
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                Session["IdUser"] = GetId;
                Session["EmailUser"] = name;
                Session["FullName"] = name;
            }
           
            return RedirectToAction("Index");
        }
    }
}