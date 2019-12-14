using BackEndSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEndSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private DotrAContext db = new DotrAContext();
        public ActionResult Index()
        {
            //方法1 透過modle去傳遞
            Home result = new Home()
            {
                Selltotal = db.Products.Count(),
                Ordertotal = db.Categories.Count(),
                Prototal = db.Orders.Count()
            };

            //方法2 透過ViewBag去傳遞
            ViewBag.ProCount = db.Products.Count();
            ViewBag.CatCount = db.Categories.Count();
            ViewBag.OrdCount = db.Orders.Count();



            return View(result);
        }

        //public ActionResult Member()
        //{
        //    DotrAContext context = new DotrAContext();
        //    var member = context.Members.ToList();

        //    return View(member);
        //}

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Suppliers()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }


    }
}