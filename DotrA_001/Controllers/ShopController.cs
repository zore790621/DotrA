using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Database.Models;
using DotrA_001.Models;
using PagedList;
using DotrADb = Database.Models.DotrADb;

namespace DotrA_001.Controllers
{
    public class ShopController : Controller
    {
        private DotrADb db = new DotrADb();

        // GET: Shop
        public ActionResult Index(int page, int? CID, int? PID)
        {
            //string.IsNullOrEmpty("");
            ShopView AllList = new ShopView();
            bool IsInt_PID = (PID is null);
            int isInt_PID_Val = PID.GetValueOrDefault();

            var pro =
                from pr in db.Products
                where (IsInt_PID || pr.ProductID == isInt_PID_Val)
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

            //分頁的內容AllList.Product.ToPagedList(page, 幾個物件)
            ViewBag.MyPageList = AllList.Product.ToPagedList(page, 9);

            return View(AllList);
        }
    }
}