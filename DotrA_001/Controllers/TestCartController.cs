using DotrA_001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class TestCartController : Controller
    {
        // GET: TestCart
        public ActionResult GetCart()
        {
            var cart = HandleCart.GetCurrentCart();
            //若現在購物車沒有商品的話，新增一個假商品(為了練習)
            if (cart.CartProducts.Count == 0)
            {
             
                cart.CartProducts.Add(
                    new Models.CartProduct()
                    {
                        ProductId = 1,
                        ProductName = "測試商品",
                        ProductPrice = 50m,
                        ProductQuantity = 1
                    }
                    );
                cart.CartProducts.Add(
                   new Models.CartProduct()
                   {
                       ProductId = 2,
                       ProductName = "測試商品2",
                       ProductPrice = 3m,
                       ProductQuantity = 1
                   }
                   );
            }
            //每當你重載頁面，假如購物車已經有商品，商品數量多+一個(session儲存的緣故)
            else
            {
                foreach (var i in cart.CartProducts)
                {
                    i.ProductQuantity++;
                }
            }
            return Content(string.Format($"目前金額為{cart.TotalAmount}元"));
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}