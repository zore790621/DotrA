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
    public class ShopDescriptionController : Controller
    {
        // GET: ShopDescription
        private DotrADb db = new DotrADb();
        [WebMethod(EnableSession = true)]
        //[Authorize]
        public ActionResult Index(int productid)
        {
            //bool toint = int.TryParse(((System.Security.Claims.ClaimsIdentity)User.Identity).RoleClaimType, out int UID);
            //var source = db.Members.FirstOrDefault(x => x.MemberID == UID);
            var productorder = db.Products.FirstOrDefault(x => x.ProductID == productid);

            var shopcartresult = new ShopCartOrderView()
            {
                ProductId = productorder.ProductID,
                ProductName = productorder.ProductName,
                Picture = productorder.Picture,
                Price = productorder.SalesPrice,
                Description = productorder.Description,
            };

            return View(shopcartresult);
        }
    }
}

     