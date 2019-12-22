using Database.Models;
using DotrA_001.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotrA_001.Controllers
{
    public class HomeController : Controller
    {
        private DotrADb db = new DotrADb();
        public ActionResult Index(int? CID, int? PID)
        {
            //string.IsNullOrEmpty("");

            //bool IsInt_PID = int.TryParse(PID.ToString(), out int result);
            bool IsInt_PID = (PID is null);
            int isint_pid_val = PID.GetValueOrDefault();

            var pro =
                from pr in db.Products
                where (pr.Status == "上架" && IsInt_PID || pr.ProductID == isint_pid_val)
                select new ProductView
                {
                    CategoryID = pr.CategoryID,
                    Description = pr.Description,
                    Picture = pr.Picture,
                    ProductID = pr.ProductID,
                    ProductName = pr.ProductName,
                    SupplierID = pr.SupplierID,
                    SalesPrice = pr.SalesPrice,
                    Status = pr.Status
                };
            
            return View(pro);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}