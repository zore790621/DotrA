using BackEndSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEndSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Member()
        //{
        //    DotrAContext context = new DotrAContext();
        //    var member = context.Members.ToList();

        //    return View(member);
        //}

        public ActionResult ProductList()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }

        public ActionResult OrderList()
        {
            return View();
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult Suppliers()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View();
        }


    }
}