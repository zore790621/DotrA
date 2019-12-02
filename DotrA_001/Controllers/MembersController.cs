using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Database.Models;

namespace DotrA_001.Controllers
{
    public class MembersController : Controller
    {
        private DotrADb db = new DotrADb();

        // GET: Members
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }
        
        // GET: Members/Register
        [AllowAnonymous]//在執行Action時,略過驗證/授權的處理
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Member member)
        {
            if (ModelState.IsValid)
            {
                //using (DotrADbContext db = new DotrADbContext())
                //{ }
                db.Members.Add(member);
                db.SaveChanges();



                ModelState.Clear();//清除 (包含錯誤訊息與模型繫結的資料都會被清空)
                ViewBag.Message = "恭喜!!  " + member.MemberAccount + "  已成功註冊";
            }
            return View();
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,MemberAccount,Password,Name,Email,Address,Phone")] Member members)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(members);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(members);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemberID,MemberAccount,Password,Name,Email,Address,Phone")] Member members)
        {
            if (ModelState.IsValid)
            {
                db.Entry(members).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(members);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member members = db.Members.Find(id);
            if (members == null)
            {
                return HttpNotFound();
            }
            return View(members);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member members = db.Members.Find(id);
            db.Members.Remove(members);
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
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Member member)
        {



            //using (DotrADbContext db = new DotrADbContext())
            //{ }
            var user = db.Members.Where(x => x.MemberAccount == member.MemberAccount && x.Password == member.Password).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(member.MemberAccount, false);// createPersistentCookie:false(不要記住我)
                Session["MemberID"] = user.MemberID.ToString();
                Session["MemberAccount"] = user.MemberAccount.ToString();
                return RedirectToAction("LoggedIn");
            }
            else
            {
                ModelState.AddModelError("", " 帳號或密碼輸入錯誤.");
            }



            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["MemberID"] != null)
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
            return RedirectToAction("Login");
        }
    }
}
