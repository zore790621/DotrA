using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using DotrA_001.Models;
using System.Security.Cryptography;
using System.Text;
using DotrA_001.Helper;
using DotrA_001.Models.ViewModels;
using System.Net.Mail;
using System.Web.Helpers;
using Database.Models;
using Microsoft.Owin.Security;
using System.Security.Claims;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

namespace DotrA_001.Controllers
{
    /*public class IdServer {
        public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>();

            // create required claims
            claims.Add(new Claim(ClaimTypes.NameIdentifier, appUserState.UserId));
            claims.Add(new Claim(ClaimTypes.Name, appUserState.Name));

            // custom – my serialized AppUserState object
            claims.Add(new Claim("userState", appUserState.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        public void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                                            DefaultAuthenticationTypes.ExternalCookie);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
    }*/
    public class MembersController : Controller
    {
        private DotrADb db = new DotrADb();

        // GET: Members
        [Authorize(Users = "admin")]//必須有授權/認證才能進入
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }
        #region ===註冊 Register===
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
        #region ===驗證帳號 VerifyAccount===
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
        #region ===登入Login/登出Logout===
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
            var user = db.Members.Where(x => x.MemberAccount == login.MemberAccount && x.Password != null).FirstOrDefault();
            if (user != null)
            {

                if (!user.EmailVerified)
                {
                    TempData["Message"] = "請先到信箱啟動驗證. Please verify your email first.";
                    return Redirect(Request.UrlReferrer.ToString());
                }

                //hash加密
                var HCode = user.HashCode;
                var encodingPasswordString = hash.EncodePassword(login.Password, HCode);

                //Check Login Detail MemberAccount and Password    
                //var Account = (from s in db.Members where (s.MemberAccount == login.MemberAccount  && s.Password.Equals(encodingPasswordString)) select s).FirstOrDefault();
                var Account = db.Members.Where(x => x.MemberAccount == login.MemberAccount && x.Password.Equals(encodingPasswordString)).FirstOrDefault();
                if (Account != null)
                {
                    #region ===驗證票證===
                    ////為所提供的使用者名稱建立驗證票證，並將該票證加入至回應的Cookie,或加入至URL
                    ////FormsAuthentication.SetAuthCookie(member.MemberAccount, false);// createPersistentCookie:false(不要記住我)
                    ////Session["MemberAccount"] = user.MemberAccount.ToString();
                    ////Session["MemberID"] = user.MemberID;//為了修改會員資料

                    //int timeout = login.RememberMe ? 1440 : 10; // 525600 min = 1 year
                    //var ticket = new FormsAuthenticationTicket(
                    //    version: 1,
                    //    name: login.MemberAccount, //可以放使用者Id
                    //    issueDate: DateTime.UtcNow,//現在UTC時間
                    //    expiration: DateTime.UtcNow.AddHours(timeout),//Cookie有效時間=現在時間往後+1小時
                    //    isPersistent: login.RememberMe,// 是否要記住我 true or false
                    //    userData: user.MemberID.ToString(), //可以放使用者角色名稱
                    //    cookiePath: FormsAuthentication.FormsCookiePath
                    //    );
                    //string encrypted = FormsAuthentication.Encrypt(ticket);
                    //var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                    //cookie.Expires = DateTime.Now.AddMinutes(timeout);
                    //cookie.HttpOnly = true;
                    //Response.Cookies.Add(cookie);

                    var ident = new ClaimsIdentity(
                    new[] { 
								// adding following 2 claim just for supporting default antiforgery provider
								new Claim(ClaimTypes.NameIdentifier, user.Email),
                                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                        
                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Email, user.Email),
								// optionally you could add roles if any
								new Claim(ClaimTypes.Role, "User")
                    },
                    CookieAuthenticationDefaults.AuthenticationType, "Local", user.MemberID.ToString());

                    HttpContext.GetOwinContext().Authentication.SignIn(
                                new AuthenticationProperties { AllowRefresh = true, IsPersistent = false }, ident);
                    #endregion
                    return RedirectToAction("Index", "Home");
                    //return RedirectToAction("Index", "Members");
                }

                TempData["Message"] = "帳號或密碼輸入錯誤!! Invallid Account or Password.";
                return Redirect(Request.UrlReferrer.ToString());
                //return View();
            }

            TempData["Message"] = "帳號或密碼輸入錯誤!! Invallid Account or Password.";
            return Redirect(Request.UrlReferrer.ToString());
            //ViewBag.Message = "帳號或密碼輸入錯誤!! Invallid Account or Password.";
            //return View();
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
            //FormsAuthentication.SignOut();//登出,移除身份驗證資料cookie 
            HttpContext.GetOwinContext().Authentication.SignOut(
                        new AuthenticationProperties { IsPersistent = false });

            //HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //Session.Abandon();//清除伺服器記憶體中的 Session  
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region ===判斷Email存在 IsEmailExist===
        [NonAction]
        public bool IsEmailExist(string email)
        {
            var isExist = db.Members.Where(x => x.Email == email).FirstOrDefault();
            return isExist != null;
        }
        #endregion
        #region ===寄送驗證Email/重設密碼Email SendVerificationLinkEmailorSendResetPasswordLinkEmail===
        [NonAction]
        public void SendVerifyOrResetEmail(string Email, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Members/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("minishopbs@gmail.com", "DotrA");
            var toEmail = new MailAddress(Email);
            var fromEmailPassword = "edaybsazwbmogabr"; // Replace with actual password

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "您的會員帳號已成功建立! Your account is successfully created!";
                body = "親愛的會員您好，很高興告訴您，您的DotrA帳號已成功建立，請點擊下方連結進行帳號驗證.<br/><br/>" +
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
        #region ===忘記密碼 ForgotPassword===
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
                message = "此信箱尚未存在. Email not found.";
            }

            ViewBag.Message = message;
            return View();
        }
        #endregion
        #region ===重設密碼 ResetPassword===
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
                using (DotrADb db = new DotrADb())
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
        #region ===修改會員資料===
        public ActionResult EditProfile(int? id)
        {
            if (id == null || ((System.Security.Claims.ClaimsIdentity)User.Identity).RoleClaimType != id.ToString())
            {
                return RedirectToAction("Index", "Home");
            }
            Member member = db.Members.Find(id);

            EditMemberVM vm = new EditMemberVM()
            {
                MemberID = member.MemberID,
                MemberAccount = member.MemberAccount,
                MemberName = member.Name,
                Phone = member.Phone,
                Address = member.Address,
                Email = member.Email
            };

            if (member == null)
            {
                return HttpNotFound();
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditMemberVM vm)
        {
            if (ModelState.IsValid)
            {
                Member member = db.Members.Find(vm.MemberID);

                member.MemberID = vm.MemberID;
                member.Name = vm.MemberName;
                member.Phone = vm.Phone;
                member.Address = vm.Address;
                member.Email = vm.Email;

                //db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("EditProfile");
            };

            return View(vm);
        }
        #endregion
        #region ===查看會員訂單===
        public ActionResult SelfOrder(int? id)
        {
            var om = db.Orders.Where(x => x.MemberID == id);
            
            var models = om.Select(x => new SelfOrderVM()
            {
                OrderID = x.OrderID,
                MemberName = x.Member.Name,
                OrderDate = x.OrderDate,
                TotalPrice = x.OrderDetails.Sum(y => y.SubTotal),
                ShipperName = x.Shipper.ShipperName,
                PaymentMethod = x.Payment.PaymentMethod,
                PaymentStatus = x.PaymentStatus
            }).ToList();

            if(om==null)
            {
                TempData["Order"] = "無訂單紀錄";
            }
            return View(models);
        }
        public ActionResult LookOrderDetails(int? id)
        {
            if (id == null )
            {
                return RedirectToAction("Index", "Home");
            }
            Order o = db.Orders.Find(id);
            OrderDetailVM vm = new OrderDetailVM()
            {
                OrderID = o.OrderID,
                OrderProducts = o.OrderDetails.Select(x => new OrderProductVM
                {
                    Discount = x.Discount,
                    SalesPrice = x.Product.SalesPrice,
                    ProductName = x.Product.ProductName,
                    Quantity = x.Quantity,
                    SubTotal = x.SubTotal
                }),
                MemberID=o.MemberID,
                RecipientName = o.RecipientName,
                RecipientPhone = o.RecipientPhone,
                RecipientAddress = o.RecipientAddress,
                MemberName = o.Member.Name,
                OrderDate = o.OrderDate,
                TotalPrice = o.OrderDetails.Sum(y => y.SubTotal),
                ShipperName = o.Shipper.ShipperName,
                PaymentMethod = o.Payment.PaymentMethod,
                PaymentStatus = o.PaymentStatus
            };

            if (vm == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }
        #endregion
        #region ===Google Login===
        public void SignIn(string ReturnUrl = "/", string type = "")
        {
            if (!Request.IsAuthenticated)
            {
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Members/GoogleLoginCallback" }, "Google");
                }
            }
        }
        [AllowAnonymous]
        public ActionResult GoogleLoginCallback()
        {
            var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;

            var loginInfo = GoogleLoginViewModel.GetLoginInfo(claimsPrincipal);
            if (loginInfo == null)
            {
                return RedirectToAction("Index");
            }

            var user = db.Members.FirstOrDefault(x => x.MemberAccount == loginInfo.nameidentifier);

            if (user == null)
            {
                user = new Member
                {
                    Email = loginInfo.emailaddress,
                    Name = loginInfo.givenname + loginInfo.surname,
                    MemberAccount = loginInfo.nameidentifier,
                    EmailVerified= true,
                    Password= "Google",
                    HashCode=loginInfo.nameidentifier,
              Address="Google",
              Phone="Google"
                };
                db.Members.Add(user);
                db.SaveChanges();
            }

            var ident = new ClaimsIdentity(
                    new[] { 
								// adding following 2 claim just for supporting default antiforgery provider
								new Claim(ClaimTypes.NameIdentifier, user.Email),
                                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                                new Claim(ClaimTypes.Name, user.Name),
                                new Claim(ClaimTypes.Email, user.Email),
								// optionally you could add roles if any
								new Claim(ClaimTypes.Role, "User")
                    },
                    CookieAuthenticationDefaults.AuthenticationType, "Google", user.MemberID.ToString());


            HttpContext.GetOwinContext().Authentication.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
            return Redirect("~/");

        }
        public ActionResult GoogleLogout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("https://www.google.com/accounts/Logout?continue=https://appengine.google.com/_ah/logout?continue=https://dotrawebsite.azurewebsites.net/Members/Logout");
        }
        #endregion
        #region ===CRUD===
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

