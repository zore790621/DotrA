using BackEndSystem.Models;
using BackEndSystem.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEndSystem.Controllers
{
    public class OrderController : Controller
    {
        // GET: Orders
        public ActionResult Index()
        {
            DotrAContext db = new DotrAContext();
            var models = db.Orders.Select(x => new OrderIndex()
            {
                OrderID = x.OrderID,
                MemberName = x.Members.Name,
                OrderDate = x.OrderDate,
                TotalPrice = x.OrderDetails.Sum(y => y.SubTotal),
                ShipperName = x.Shippers.ShipperName
            }).ToList();
            return View(models);
        }
    }
}