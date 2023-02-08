using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Question_Answer_Engine.Models;

namespace Question_Answer_Engine.Controllers
{
    public class AccountController : Controller
    {
        private QA_Engine_DbContext db = new QA_Engine_DbContext();

        // GET: Account/Login
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        // POST: Account/Login
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var query = (from u in db.Users
                             where u.UserName.Equals(userName) && u.Password.Equals(password)
                             select u).SingleOrDefault();
                if(query == null)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    ViewBag.ReturnUrl = ReturnUrl;
                    return View();
                }

                FormsAuthentication.RedirectFromLoginPage(userName, false);
            }

            return View();
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "username,password,lastname,firstname,email,country")] User user)
        {
            if (ModelState.IsValid)
            {
                var query = (from u in db.Users
                             where u.UserName.Equals(user.UserName)
                             select u).FirstOrDefault();
                if (query != null)
                {
                    ModelState.AddModelError("", "This username already exists");
                    return View(user);
                }
                db.Users.Add(user);
                db.SaveChanges();
                FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
            }
            else
                ModelState.AddModelError("", "Invalid input");
            return View(user);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool dispose)
        {
            if (dispose)
            {
                db.Dispose();
            }
            base.Dispose(dispose);
        }
    }
}