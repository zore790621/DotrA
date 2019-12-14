using BackEndSystem.Models;
using BackEndSystem.Models.ViewModel;
using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BackEndSystem.Controllers
{
    [Authorize]
    public class LogInController : Controller
    {
        private DotrADb db = new DotrADb();
        // GET: BackEnd
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult LogIn()
        {

            return View();
        }

        [HttpPost,AllowAnonymous, ValidateAntiForgeryToken, AntiForgeryErrorHandler]
        public ActionResult LogIn(LogInRequest data)
        {
            string account = HttpUtility.HtmlEncode(data.AdminAccount);
            string password = HttpUtility.HtmlEncode(data.AdminPW);

            var user = db.Admins.FirstOrDefault(x => x.AdminAccount == account && x.AdminPW == password);
            if ( user != null)
            {
                var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: data.AdminAccount, //可以放使用者Id
                issueDate: DateTime.UtcNow,//現在UTC時間
                expiration: DateTime.UtcNow.AddHours(1),//Cookie有效時間=現在時間往後+1小時
                isPersistent: true,// 是否要記住我 true or false
                userData: data.AdminAccount, //可以放使用者角色名稱
                cookiePath: FormsAuthentication.FormsCookiePath);
                var encryptedTicket = FormsAuthentication.Encrypt(ticket); //把驗證的表單加密
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(cookie);
                return Redirect("/home/Index");
            }
            else
            {
                ModelState.AddModelError("", "帳號或密碼錯誤");
                return View();
            }
           
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("LogIn", "LogIn");
        }
    }
}