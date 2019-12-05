using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotrA_001.Models;
using System.Security.Cryptography;
using System.Text;
using DotrA_001.Helper;
using DotrA_001.Models.ViewModels;
using System.Net.Mail;
using System.Web.Helpers;

namespace DotrA_001.Controllers
{
    public class MembersController : Controller
    {
        private DotrADbContext db = new DotrADbContext();

        // GET: Members
        [Authorize(Users ="admin")]//必須有授權/認證才能進入
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }
        #region 註冊 Register
        // GET: Members/Register
        //[AllowAnonymous]//在執行Action時,略過驗證/授權的處理
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Exclude = "EmailVerified,ActivationCode")]Member member)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                #region //檢查信箱是否已經存在
                var isExist = IsEmailExist(member.Email);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email已經存在請更換!! This Email already exist");
                    return View();
                }
                #endregion
                #region //檢查帳號是否已經存在
                //var registerAccount = (from m in db.Members where m.MemberAccount.Equals(member.MemberAccount) select m).SingleOrDefault();
                var registerAccount = db.Members.Where(x => x.MemberAccount.Equals(member.MemberAccount)).SingleOrDefault();
                if (registerAccount != null)
                {
                    ModelState.AddModelError("AccountExist", "此帳號已經存在，請更換帳號!! This Account already registered.");
                    //ViewBag.Message = "此帳號已經存在，請更換帳號!!";
                    return View();
                }
                #endregion
                #region //產生 Activation Code 
                member.ActivationCode = Guid.NewGuid();
                //var keyNew = hash.GeneratePassword(10);
                #endregion
                #region //hash 加密
                var keyNew = hash.GeneratePassword(10);
                member.HashCode = keyNew;
                var password = hash.EncodePassword(member.Password, keyNew);
                member.Password = password;
                #endregion
                member.EmailVerified = false;//註冊時將Email認證設為false

                db.Members.Add(member);
                db.SaveChanges();
                #region //寄送帳號啟用信 Send Email to Account
                SendVerifyOrResetEmail(member.Email, member.ActivationCode.ToString());
                message = "註冊成功，驗證帳號連結已寄到您的信箱. Registration successfully done. Account activation link " +
                    " has been sent to your Email : " + member.Email; /*"恭喜!!  " + member.MemberAccount + "  已成功註冊"*/
                #endregion

                ModelState.Clear();//清除 (包含錯誤訊息與模型繫結的資料都會被清空)
                ViewBag.Message = message;
                //return RedirectToAction("Login");
            }
            return View();
        }
        #endregion
        #region 驗證帳號 VerifyAccount
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;

            db.Configuration.ValidateOnSaveEnabled = false; // This line I have added here to avoid 
                                                            // Confirm password does not match issue on save changes
            var v = db.Members.Where(x => x.ActivationCode == new Guid(id)).FirstOrDefault();
            if (v != null)
            {
                v.EmailVerified = true;
                db.SaveChanges();
                Status = true;
            }
            else
            {
                ViewBag.Message = "無效的操作。Invalid Request.";
            }

            ViewBag.Status = Status;
            return View();
        }
        #endregion
        #region 登入Login/登出Logout
        // GET: Members/Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Member member, LoginVM login)
        {

            //var user = db.Members.Where(x => x.MemberAccount == member.MemberAccount && x.Password == member.Password).FirstOrDefault();//密碼未加密規則
            //var user = (from s in db.Members where s.MemberAccount == login.MemberAccount select s).FirstOrDefault();
            var user = db.Members.Where(x => x.MemberAccount == login.MemberAccount).FirstOrDefault();
            if (user != null)
            {
                if (!user.EmailVerified)
                {
                    ViewBag.Message = "請先到信箱啟動驗證. Please verify your email first.";
                    return View();
                }
                
                //hash加密
                var HCode = user.HashCode;
                var encodingPasswordString = hash.EncodePassword(login.Password, HCode);

                //Check Login Detail MemberAccount and Password    
                //var Account = (from s in db.Members where (s.MemberAccount == login.MemberAccount  && s.Password.Equals(encodingPasswordString)) select s).FirstOrDefault();
                var Account = db.Members.Where(x => x.MemberAccount == login.MemberAccount && x.Password.Equals(encodingPasswordString)).FirstOrDefault();
                if (Account != null)
                {
                    #region 驗證票證
                    //為所提供的使用者名稱建立驗證票證，並將該票證加入至回應的Cookie,或加入至URL
                    //FormsAuthentication.SetAuthCookie(member.MemberAccount, false);// createPersistentCookie:false(不要記住我)
                    //Session["MemberID"] = user.MemberID.ToString();
                    //Session["MemberAccount"] = user.MemberAccount.ToString();

                    int timeout = login.RememberMe ? 525600 : 20; // 525600 min = 1 year
                    var ticket = new FormsAuthenticationTicket(login.MemberAccount, login.RememberMe, timeout);
                    string encrypted = FormsAuthentication.Encrypt(ticket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    #endregion
                    return RedirectToAction("LoggedIn");
                    //return RedirectToAction("Index", "Members");
                }
                ViewBag.Message = "帳號或密碼輸入錯誤!! Invallid Account or Password.";
                return View();
            }

            ViewBag.Message = "帳號或密碼輸入錯誤!! Invallid Account or Password.";
            //ModelState.AddModelError("", "帳號或密碼輸入錯誤!!")
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();//登出,移除身份驗證資料cookie 
            //Session.Abandon();//清除伺服器記憶體中的 Session  
            return RedirectToAction("Login", "Members");
        }
        #endregion
        #region 判斷Email存在 IsEmailExist
        [NonAction]
        public bool IsEmailExist(string email)
        {
            var isExist = db.Members.Where(x => x.Email == email).FirstOrDefault();
            return isExist != null;
        }
        #endregion
        #region 寄送驗證Email/重設密碼Email SendVerificationLinkEmailorSendResetPasswordLinkEmail
        [NonAction]
        public void SendVerifyOrResetEmail(string Email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Members/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("minishopbs@gmail.com", "MiniShop");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "edaybsazwbmogabr"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "您的會員帳號已成功建立! Your account is successfully created!";
                body = "親愛的會員您好，很高興告訴您，您的MiniShop帳號已成功建立，請點擊下方連結進行帳號驗證.<br/><br/>" +
                    "Hi,<br/><br/>We are excited to tell you that your MiniShop account is" +
                    " successfully created. Please click on the below link to verify your account" +
                    " <br/><br/><a href=" + link + ">請點此驗證帳號. Account activation link</a> ";

            }
            else if (emailFor == "ResetPassword")
            {
                subject = "重設密碼! Reset Password!";
                body = "親愛的會員您好,我們收到您重設密碼的請求，請點擊下方連結重設密碼.<br/><br/>" +
                    "Hi,<br/><br/>We got request for reset your account password. Please click on the below link to reset your password" +
                    "<br/><br/><a href=" + link + ">請點此重設密碼. Reset Password link</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        #endregion
        #region 忘記密碼 ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            //Verify Email ID
            //Generate Reset password link 
            //Send Email 
            string message = "";
            bool status = false;

            var account = db.Members.Where(x => x.Email == Email).FirstOrDefault();
            if (account != null)
            {
                //Send email for reset password
                string resetCode = Guid.NewGuid().ToString();
                SendVerifyOrResetEmail(account.Email, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                //This line I have added here to avoid confirm password not match issue , as we had added a confirm password property 
                //in our model class in part 1
                db.Configuration.ValidateOnSaveEnabled = false;
                db.SaveChanges();
                message = "重設密碼連結已寄到您的信箱. Reset password link has been sent to your Email.";
            }
            else
            {
                message = "此帳號不存在. Account not found.";
            }

            ViewBag.Message = message;
            return View();
        }
        #endregion
        #region 重設密碼 ResetPassword
        public ActionResult ResetPassword(string id)
        {
            //Verify the reset password link
            //Find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }

            var user = db.Members.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
            if (user != null)
            {
                ResetPasswordVM model = new ResetPasswordVM();
                model.ResetCode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordVM model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                using (DotrADbContext db = new DotrADbContext())
                {
                    var user = db.Members.Where(x => x.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        var keyNew = hash.GeneratePassword(10);
                        user.HashCode = keyNew;
                        var password = hash.EncodePassword(model.NewPassword, keyNew);
                        user.Password = password;

                        user.ResetPasswordCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        message = "新密碼已成功更新. New password updated successfully.";
                    }
                }

            }
            else
            {
                message = "無效的操作. Something invalid.";
            }
            ViewBag.Message = message;
            return View();
        }
        #endregion
        #region CRUD
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }
        //// POST: Members/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MemberID,MemberAccount,Password,Name,Email,Address,Phone")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Members.Add(member);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(member);
        //}

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Exclude = "EmailVerified")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete([Bind(Exclude = "EmailVerified")]int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Exclude = "EmailVerified")]int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
        #endregion
    }
}

