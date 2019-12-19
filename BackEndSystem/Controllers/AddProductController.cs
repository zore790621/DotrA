using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackEndSystem.Models.ViewModel;
using Database.Models;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Models;

namespace BackEndSystem.Controllers
{
    public class AddProductController : Controller
    {
        private DotrADb db = new DotrADb();
        private string eab5006010b755e9e0a81a850f6922dae1dea998;

        // GET: AddProes
        public ActionResult Index()
        {

            //var products = db.Products.ToList();
            List<AddPro> viewModel = db.Products.Select(x => new AddPro
            {
                ProductId = x.ProductID,
                PName = x.ProductName,
                SupplierID = x.SupplierID,
                CategoryID = x.CategoryID,
                PPrice = x.UnitPrice,
                Pdescript = x.Description,
                PQuantity = x.Quantity,
                PSalesPrice = x.SalesPrice

            }).ToList();
            return View(viewModel);
        }

        // GET: AddProes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var query = from p in db.Products
                            //join c in db.Categories
                            //on p.CategoryID equals c.CategoryID
                        where p.ProductID == id
                        select new AddPro()
                        {
                            ProductId = p.ProductID,
                            PName = p.ProductName,
                            SupplierID = p.SupplierID,
                            CategoryID = p.CategoryID,
                            PPrice = p.UnitPrice,
                            Pdescript = p.Description,
                            PQuantity = p.Quantity,
                            PSalesPrice = p.SalesPrice
                        };
            var listQuery = query.ToList();



            if (listQuery.Count() == 0)
            {
                return HttpNotFound();
            }


            return View(listQuery);
        }

        // GET: AddProes/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: AddProes/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddPro addPro)
        {
            var picture = upload(addPro.Picture);
            var CLIENT_ID = System.Configuration.ConfigurationManager.AppSettings["Imgur_CLIENT_ID"];
            var CLIENT_SECRET = System.Configuration.ConfigurationManager.AppSettings["Imgur_CLIENT_SECRET"];

            if (ModelState.IsValid)
            {

                Product p = new Product();


                p.ProductName = addPro.PName;
                p.SupplierID = addPro.SupplierID;
                p.CategoryID = addPro.CategoryID;
                p.UnitPrice = addPro.PPrice;
                p.Picture = picture;
                p.Description = addPro.Pdescript;
                p.Quantity = addPro.PQuantity;
                p.SalesPrice = addPro.PSalesPrice;
                p.Status = "未上架";


                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index", "Products");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", addPro.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", addPro.SupplierID);

            return View(addPro);
        }

        // 新增產品圖片的方法
        public string upload(HttpPostedFileBase[] photos)
        {

            string path = "";
            //string fileName = string.Empty;
            IImage image;

            if (photos != null)
            {
                foreach (var photo in photos)
                {
                    var client = new ImgurClient("824755358e45627", "eab5006010b755e9e0a81a850f6922dae1dea998");
                    var endpoint = new ImageEndpoint(client);
                    image = endpoint.UploadImageStreamAsync(photo.InputStream).GetAwaiter().GetResult();
                    path += image.Link + ",";
                }
                path = path.Substring(0, path.Length - 1);
            }
            return (path);/*fileName*/
        }



        //var fileName = Path.GetFileName(photo.FileName);
        //var image = endpoint.UploadImageStreamAsync(fileName).GetAwaiter().GetResult();

        //foreach (var file in photo)
        //{
        //    if (file != null)
        //    //if (photo.ContentLength > 0)
        //    {
        //        //取得檔案名稱
        //        fileName = Path.GetFileName(file.FileName);
        //        path = Path.Combine(Server.MapPath(trail), fileName);
        //        file.SaveAs(path);
        //        TempData["message"] = "上傳成功";

        //        //刪除上傳的圖檔
        //        //string filePath = Server.MapPath("~/Photos" + PathDB);
        //        //System.IO.File.Delete(filePath);
        //    }
        //    else
        //    {
        //        TempData["message"] = "請先選擇檔案";
        //    }
        //}

        // GET: AddProes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //AddPro addPro = db.AddProes.Find(id);

            var query = from p in db.Products
                        join c in db.Categories
                        on p.CategoryID equals c.CategoryID
                        where p.ProductID == id
                        select new AddPro()
                        {
                            ProductId = p.ProductID,
                            PName = p.ProductName,
                            SupplierID = p.SupplierID,
                            CategoryID = p.CategoryID,
                            PPrice = p.UnitPrice,
                            Pdescript = p.Description,
                            PQuantity = p.Quantity,
                            PSalesPrice = p.SalesPrice
                        };
            var listQuery = query.ToList();

            if (listQuery.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(listQuery);
        }

        // POST: AddProes/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AddPro addPro)
        {
            if (ModelState.IsValid)
            {
                Product p = new Product();
                Category c = new Category();

                p.ProductName = addPro.PName;
                p.SupplierID = addPro.SupplierID;
                p.CategoryID = addPro.CategoryID;
                p.UnitPrice = addPro.PPrice;
                p.Description = addPro.Pdescript;
                p.Quantity = addPro.PQuantity;
                p.SalesPrice = addPro.PSalesPrice;

                db.Entry(p).State = EntityState.Modified;
                db.Entry(c).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addPro);
        }

        // GET: AddProes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var query = from p in db.Products
                            //join c in db.Categories
                            //on p.CategoryID equals c.CategoryID
                        where p.ProductID == id
                        select new AddPro()
                        {
                            ProductId = p.ProductID,
                            PName = p.ProductName,
                            SupplierID = p.SupplierID,
                            CategoryID = p.CategoryID,
                            PPrice = p.UnitPrice,
                            Pdescript = p.Description,
                            PQuantity = p.Quantity,
                            PSalesPrice = p.SalesPrice
                        };
            var listQuery = query.ToList();

            if (listQuery.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(listQuery);
        }

        // POST: AddProes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product p = db.Products.Find(id);
            db.Products.Remove(p);
            //Categories c = db.Categories.Find(id);
            //db.Categories.Remove(c);

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
