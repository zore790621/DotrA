using BackEndSystem.Attributes;
using BackEndSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Models;

namespace BackEndSystem.Controllers
{
    public class OrdersController : Controller
    {
        DotrADb db = new DotrADb();
        // GET: Orders
        public ActionResult Index()
        {
           
            var models = db.Orders.Select(x => new OrderIndex()
            {
                OrderID = x.OrderID,
                MemberName = x.Member.Name,
                OrderDate = x.OrderDate,
                TotalPrice = x.OrderDetails.Sum(y => y.SubTotal),
                ShipperName = x.Shipper.ShipperName,
                PaymentMethod = x.Payment.PaymentMethod
            }).ToList();
            return View(models);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order o = db.Orders.Find(id);
            OrderDetailVM vm = new OrderDetailVM()
            {
                OrderID = o.OrderID,
                OrderProducts = o.OrderDetails.Select(x => new OrderProductVM
                {
                    Discount = x.Discount,
                    SalesPrice = x.Product.SalesPrice,
                    ProductName = x.Product.ProductName,
                    Quantity = x.Quantity,
                    SubTotal = x.SubTotal
                }),
                RecipientName = o.RecipientName,
                RecipientPhone = o.RecipientPhone,
                RecipientAddress = o.RecipientAddress,
                MemberName = o.Member.Name,
                OrderDate = o.OrderDate,
                TotalPrice = o.OrderDetails.Sum(y => y.SubTotal),
                ShipperName = o.Shipper.ShipperName,
                PaymentMethod = o.Payment.PaymentMethod
            };

            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        [Redirect(Users = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order o = db.Orders.Find(id);
            OrderDetailVM vm = new OrderDetailVM()
            {
                OrderID = o.OrderID,
                OrderProducts = o.OrderDetails.Select(x => new OrderProductVM
                {
                    Discount = x.Discount,
                    SalesPrice = x.Product.SalesPrice,
                    ProductName = x.Product.ProductName,
                    Quantity = x.Quantity,
                    SubTotal = x.SubTotal
                }),
                RecipientName = o.RecipientName,
                RecipientPhone = o.RecipientPhone,
                RecipientAddress = o.RecipientAddress
            };
            if (vm == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order o = db.Orders.Find(id);
            var od = db.OrderDetails.Where(x => x.OrderID == id);
            foreach (var d in od)
            {
                db.OrderDetails.Remove(d);
            }
            db.Orders.Remove(o);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}