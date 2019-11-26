using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001_Clone.Controllers
{
    public class ShoppingCarController : Controller
    {
        // GET: ShoppingCar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrductCheckoutView()
        {
            return View();
        }
        public ActionResult PrductCheckButton()
        {
            return View();
        }
    }
}