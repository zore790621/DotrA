using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class SanTestCartController : Controller
    {
        // GET: SanTestCart
        public ActionResult Index()
        {
            return View();
        }


        //取得目前購物車頁面
        public ActionResult GetCart()
        {
            return PartialView("_CartPartial");
        }

        //以id加入Product至購物車，並回傳購物車頁面
        public ActionResult AddToCart(int id)
        {
            var currentCart = Models.Operation.GetCurrentCart();
            currentCart.AddProduct(id);
            return PartialView("_CartPartial");
        }
        //以id刪除Product至購物車，並回傳購物車頁面
        public ActionResult RemoveFromCart(int id)
        {
            var currentCart = Models.Operation.GetCurrentCart();
            currentCart.RemoveProduct(id);
            return PartialView("_CartPartial");
        }

    }
}