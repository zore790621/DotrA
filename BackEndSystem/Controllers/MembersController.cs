
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BackEndSystem.Attributes;
using BackEndSystem.Models.ViewModel;
using Database.Models;

namespace BackEndSystem.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private DotrADb db = new DotrADb();

        // GET: Members
        public ActionResult Index()
        {
            var models = db.Members.Select(x => new MemberIndex()
            {
                MemberID = x.MemberID,
                MemberAccount = x.MemberAccount,
                MemberName = x.Name,
                Email = x.Email,
                Address = x.Address,
                Phone = x.Phone
            }).ToList();

            return View(models);
        }

        // GET: Members/Details/5
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
        [Redirect(Users ="Admin")]
        // GET: Members/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemberID,MemberAccount,Password,Name,Email,Address,Phone")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: Members/Edit/5
        [Redirect(Users = "Admin")]
        public ActionResult Edit(int? id)
        {
            //if(User.IsInRole(admin))
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);

            MemberIndex vm = new MemberIndex()
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
        [Authorize(Users = "admin")]
        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberIndex vm)
        {
            if (ModelState.IsValid)
            {
                Member member = db.Members.Find(vm.MemberID);

                member.MemberID = vm.MemberID;
                member.MemberAccount = vm.MemberAccount;
                member.Name = vm.MemberName;
                member.Phone = vm.Phone;
                member.Address = vm.Address;
                member.Email = vm.Email; 
              
                //db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            };
        
            return View(vm);
        }

        // GET: Members/Delete/5
        [Redirect(Users = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            MemberIndex vm = new MemberIndex()
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

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
    }
}
