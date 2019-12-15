using Database.Models;
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
        private DotrADb db = new DotrADb();
        // GET: ShoppingCar
        public ActionResult Index(int? UID, int productid)
        {
            if (UID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member members = db.Members.Find(UID);
            DotrA_001.Models.Member member = new DotrA_001.Models.Member()
            {
                MemberID = members.MemberID,
                MemberAccount = members.MemberAccount,
                Address = members.Address,
                Email = members.Email,
                Name = members.Name,
                Password = members.Password,
                Phone = members.Phone,
                HashCode = members.HashCode,
                EmailVerified = members.EmailVerified,
                ActivationCode = members.ActivationCode,
                ResetPasswordCode = members.ResetPasswordCode
            };
            if (members == null)
            {
                return HttpNotFound();
            }
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