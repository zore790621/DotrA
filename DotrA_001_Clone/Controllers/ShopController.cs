using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001_Clone.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Single_Product()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        } 
        public ActionResult PageSelection()
        {
            return View();
        }
        public ActionResult PriceRange()
        {
            return View();
        }
    }
}