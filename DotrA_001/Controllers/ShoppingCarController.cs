using Database.Models;
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
    public class ShoppingCarController : Controller
    {
        private DotrADb db = new DotrADb();
        // GET: ShoppingCar
        [WebMethod(EnableSession = true)]
        public ActionResult Index(int productid)
        {
            bool toint = int.TryParse(((FormsIdentity)User.Identity).Ticket.UserData, out int UID);
            if (toint == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Member members = db.Members.FirstOrDefault(x => x.MemberID == UID);
            DotrA_001.Models.Member member = new DotrA_001.Models.Member()
            {
                                MemberID = members.MemberID,
                                Address = members.Address,
                                Email = members.Email,
                                Phone = members.Phone
            };
            if (members == null)
            {
                return HttpNotFound();
            }

            ViewBag.Payment = new SelectList(db.Payments, "PaymentID", "PaymentName");
            ViewBag.Ship = new SelectList(db.Shippers, "ShipperID", "ShipperName");
            return View(member);
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