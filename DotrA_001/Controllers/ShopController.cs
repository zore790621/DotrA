using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Models;
using DotrA_001.Models;
using DotrADb = Database.Models.DotrADb;

namespace DotrA_001.Controllers
{
    public class ShopController : Controller
    {
        private DotrADb db = new DotrADb();

        // GET: Shop
        public ActionResult Index()
        {
            ShopView AllList = new ShopView();
            var pro =
                from pr in db.Products
                select new ProductView
                {
                    CategoryID = pr.CategoryID,
                    Description = pr.Description,
                    Picture = pr.Picture,
                    ProductID = pr.ProductID,
                    ProductName = pr.ProductName,
                    SupplierID = pr.SupplierID,
                    UnitPrice = pr.UnitPrice
                };
            AllList.Product = pro.ToList();
            var cat =
                from ca in db.Categories
                select new CategoryView
                {
                    CategoryID = ca.CategoryID,
                    Picture = ca.Picture,
                    CategoryName = ca.CategoryName
                };
            AllList.Category = cat.ToList();
            var Sup =
                from su in db.Suppliers
                select new SupplierView
                {
                    SupplierID = su.SupplierID,
                    CampanyPhone = su.CampanyPhone,
                    CompanyName = su.CompanyName,
                    CompanyAddress = su.CompanyAddress
                };
            AllList.Supplier = Sup.ToList();
            return View(AllList);
        }
    }
}