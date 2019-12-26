
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database.Models;
using static System.Net.Mime.MediaTypeNames;

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
            //   Ordertotal = db.Orders.Count(),
            //   Prototal = db.Products.Count(),
            //   Cattotal = db.Categories.Count(),
            //   Selltotal=db.Orders.Where(x=>x.OrderDate.Month==DateTime.Now.Month).Sum(x=>x.OrderDetails.Sum(y => y.SubTotal))
            //};

            //var result = db.OrderDetails.GroupBy(x => x.ProductID).Select(x => new
            //{
            //    ProductName = x.FirstOrDefault().Product.ProductName,
            //    Quantity = x.Sum(y => y.Quantity),
            //    Amount = x.Sum(y => y.SubTotal)
            //}).OrderByDescending(x => x.Quantity).Take(5);
            //List<Tuple<int, int, int>> tuplesresult = new List<Tuple<int, int, int>>()
            //{
            //};

            //ViewBag.List = result;
            var result = db.OrderDetails.GroupBy(x => x.ProductID).Select(x => new
            {
                ProductName = x.FirstOrDefault().Product.ProductName,
                Quantity = x.Sum(y => y.Quantity),
                Amount = x.Sum(y => y.SubTotal)
            }).OrderByDescending(x => x.Quantity).Take(5);

            ViewBag.ProductName = result.Select(x => x.ProductName).ToArray();
            ViewBag.Quantity = result.Select(x => x.Quantity).ToArray();
            ViewBag.Amount = result.Select(x => x.Amount).ToArray();

            //方法2 透過ViewBag去傳遞
            ViewBag.ProCount = db.Products.Count();
            ViewBag.MemCount = db.Members.Count();
            ViewBag.OrdCount = db.Orders.Count();
            ViewBag.MemCount = db.Members.Count();
            ViewBag.Selltotal = db.Orders.Where(x => x.OrderDate.Month == DateTime.Now.Month).Sum(x => x.OrderDetails.Sum(y => y.SubTotal));

            //方法4. 透過這樣的方式效能比較好
            //if(Application["TotalRow"]==null)
            //{
            //    Application["TotalRow"] = db.Products.Count();

            //}
          

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

        [HttpPost]
        public JsonResult GetProductTop5()
        {
            var result = db.OrderDetails.GroupBy(x => x.ProductID).Select(x => new
            {
                ProductName = x.FirstOrDefault().Product.ProductName,
                Quantity = x.Sum(y => y.Quantity),
                Amount = x.Sum(y => y.SubTotal)
            }).OrderByDescending(x => x.Quantity).Take(8);
            return Json(result);
        }


    }
}