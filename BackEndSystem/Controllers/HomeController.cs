
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database.Models;

namespace BackEndSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
    
        private DotrADb db = new DotrADb();
        public ActionResult Index()
        {
            ////方法1 透過modle去傳遞
            //Home result = new Home()
            //{
            //   Prototal = db.Products.Count(),
            //   Cattotal = db.Categories.Count(),
            //   Ordertotal = db.Orders.Count(),
            //   Selltotal=db.Orders.Where(x=>x.OrderDate.Month==DateTime.Now.Month).Sum(x=>x.OrderDetails.Sum(y => y.SubTotal))
            //};



            //方法2 透過ViewBag去傳遞
            ViewBag.ProCount = db.Products.Count();
            ViewBag.MemCount = db.Members.Count();
            ViewBag.OrdCount = db.Orders.Count();
            ViewBag.Selltotal = db.Orders.Where(x => x.OrderDate.Month == DateTime.Now.Month).Sum(x => x.OrderDetails.Sum(y => y.SubTotal));



            return View();
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