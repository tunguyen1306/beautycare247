using BeautyAndCare.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
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
                                  select  dataProducts ;
            var queryListPicture = (from data in db.tblPictures
                                // where data.Position==1
                                 select data).ToList();

            tbl.ListMenu = queryListMenu;
            tbl.ListPicture = queryListPicture;
            tbl.tblPro = dataNewProducts.ToList();

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
            Session.Clear();
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
            return View();
        }
        [HttpPost]
        public ActionResult Register(tblUser tbluser)
        {

            //var tblUser = new tblUser();
            tbluser.IdTypeUser = 3;
            tbluser.TotalPointUser = 0;
            tbluser.SubscribeUser = true;
            //tblUser.AddressUser = tbluser.AddressUser;
            //tblUser.EmailUser = tbluser.EmailUser;
            //tblUser.FirtNameUser = tbluser.FirtNameUser;
            //tblUser.LastNameUser = tbluser.LastNameUser;
            //tblUser.SubscribeUser = tbluser.SubscribeUser;
            //tblUser.PhoneUser = tbluser.PhoneUser;
            //tblUser.PassUser = tbluser.PassUser;
            //tblUser.NameUser = tbluser.NameUser;
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
        public ActionResult Detail(string id)
        {
            var id_ = int.Parse(id.Split('-').Last());
            //var dataNewProducts = from dataProducts in db.tblProducts
            //                      join dataCategory in db.tblMenus on dataProducts.IdCategoryProducts equals dataCategory.IdMenu
            //                      join datapic in db.tblPictures on dataProducts.IdProducts equals datapic.ProductsId
            //                      where dataProducts.StatusProducts == 1 && dataProducts.IdProducts== id_
            //                      orderby dataProducts.IdProducts descending
            //                      select new tblAll{ tblProducts=dataProducts,clPicture= datapic };
            tblAll pic = new tblAll { tblProductRel= db.tblProducts.ToList(), tblPro = db.tblProducts.Where(t => t.IdProducts == id_).ToList(), ListPicture = db.tblPictures.Where(t => t.ProductsId == id_).ToList() };
            return View(pic);
        }
        public ActionResult CheckOut()
        {

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
                ShoppingCartItem cartIem = new ShoppingCartItem()
                {
                    ProductName = dataPro.NameProducts,
                    ProductID = dataPro.IdProducts,
                    Price = dataPro.PriceNewProducts,
                    Quanlity = 1,
                    Total = Convert.ToDecimal(dataPro.PriceProducts),
                    SubTotal = Convert.ToDecimal(dataPro.PriceProducts)
                };
                cart.AddToCart(cartIem);
                Session["cart"] = cart;
                return Json(dataPro);

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
        public ActionResult ListProducts( string id, int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 1;
            var id_ = int.Parse(id.Split('-').Last());
            ViewBag.TypeID = id_;
            var dataMe = (from datame in db.tblMenus
                          where datame.IdMenu==id_
                           select datame).ToList();
            var data = from dataPro in db.tblProducts
                       join dataPic in db.tblPictures on dataPro.IdProducts equals dataPic.ProductsId
                       join dataMenu in db.tblMenus on dataPro.IdCategoryProducts equals dataMenu.IdMenu
                       where dataPic.Position==1 && dataPro.IdCategoryProducts==id_
                       select new tblAll { tblProducts=dataPro,clPicture=dataPic,tblMenu=dataMenu };
           
            return View(data.ToList().ToPagedList(pagenum,pageSize));
    
        }
        public ActionResult ListProducts1(string id, int? page)
        {
            var pagenum = page ?? 1;
            var pageSize = 1;
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
    }
}