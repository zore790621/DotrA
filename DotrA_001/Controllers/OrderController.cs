using Database.Models;
using DotrA_001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services;

namespace DotrA_001.Controllers
{
    public class OrderController : Controller
    {

        private DotrADb db = new DotrADb();
        // GET: Order

        public ActionResult Index()
        {
            bool toint = int.TryParse(((FormsIdentity)User.Identity).Ticket.UserData.ToString(), out int UID);
            if (toint == false)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var source = db.Members.FirstOrDefault(x => x.MemberID == UID);

            var result = new MemderOrderViewModel()
            {
                RecipientName = source.Name,
                RecipientAddress = source.Address,
                RecipientPhone = source.Phone
            };

            ViewBag.ShipperID = new SelectList(db.Shippers, "ShipperID", "ShipperName");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentMethod");

            return View(result);
        }

        [WebMethod(EnableSession = true)]
        [HttpPost]
        public ActionResult Index(MemderOrderViewModel Alllist)
        {
            bool toint = int.TryParse(((FormsIdentity)User.Identity).Ticket.UserData.ToString(), out int UID);
            if (toint == false)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var source = db.Members.FirstOrDefault(x => x.MemberID == UID);

            if (this.ModelState.IsValid)
            {   //取得目前購物車
                var currentcart = Models.Operation.GetCurrentCart();

                //取得目前登入使用者Id
                var userId = ((FormsIdentity)User.Identity).Ticket.UserData;

                try
                {
                    //建立Order物件
                    var order = new Order()
                    {
                        MemberID = source.MemberID,
                        RecipientName = Alllist.RecipientName,
                        RecipientAddress = Alllist.RecipientAddress,
                        RecipientPhone = Alllist.RecipientPhone,
                        ShipperID = Alllist.ShipperID,
                        PaymentID = Alllist.PaymentID,
                        OrderDate = DateTime.Now
                    };
                    //加其入Orders資料表後，儲存變更
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var odtest = (from o in db.Orders
                                  where o.OrderID == order.OrderID
                                  select o).ToList().FirstOrDefault();

                    //取得購物車中OrderDetai物件
                    var orderDetails = currentcart.ToOrderDetailList(odtest.OrderID);

                    //將其加入OrderDetails資料表後，儲存變更
                    db.OrderDetails.AddRange(orderDetails);
                    db.SaveChanges();
                    currentcart.ClearCart();
                    return Content("訂購成功");
                }
                catch (Exception)
                {
                    return Content("訂購失敗");
                }
            }
            return View();
        }
    }
}