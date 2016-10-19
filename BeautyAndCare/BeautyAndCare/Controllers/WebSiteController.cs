using BeautyAndCare.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult Detail()
        {
            return View();
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
                    Price = dataPro.PriceProducts,
                    Quanlity = 1,
                    Total = Convert.ToDecimal(dataPro.PriceProducts)
                };
                cart.AddToCart(cartIem);
                Session["cart"] = cart;
                return Json(dataPro);

            }
                return Json(dataPro);
        }
        public ActionResult UpdateToCart(int ProductId,int Quantity)
        {
          
                ShoppingCart cart = (ShoppingCart)Session["cart"];
            if (cart!=null)
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
                             where data.NameCode== coupon
                             select data;
            return Json(dataCoupon.ToList());
        }

    }
}