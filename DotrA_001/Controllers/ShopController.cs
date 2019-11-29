using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotrA_001.Models;



namespace DotrA_001.Controllers
{
    public class ShopController : Controller
    {
        private tttttttContext db = new tttttttContext();

        // GET: Shop
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