using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotrA_001.Models;

namespace DotrA_001.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult GetCart()
        {
            return PartialView("_cartpartial");
        }
        public ActionResult AddtoCart(int id)
        {
            var currentCart = HandleCart.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_cartpartial");

        }
        public ActionResult Index()
        {
            return View();
        }
    }
}