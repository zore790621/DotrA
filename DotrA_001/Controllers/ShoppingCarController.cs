using DotrA_001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class ShoppingCarController : Controller
    {
        private tttttttContext db = new tttttttContext();
        // GET: ShoppingCar
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Members members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }
        public ActionResult PrductCheckoutView()
        {
            return View();
        }
        public ActionResult PrductCheckButton()
        {
            return View();
        }
        public ActionResult DroedownAccount()
        {
            return View();
        }
    }
}