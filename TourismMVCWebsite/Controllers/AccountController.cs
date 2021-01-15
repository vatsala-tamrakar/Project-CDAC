using ProjectDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TourismMVCWebsite.Models;


namespace TourismMVCWebsite.Controllers
{
    public class AccountController : Controller
    {
        TourismWebsiteDBEntities db = new TourismWebsiteDBEntities();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
           
                var result = db.Users.Where(a => a.UserName == model.UserName && a.Password == model.Password).ToList();
                if (result.Count > 0)
                {
                    Session["Id"] = result[0].UserId;
                    FormsAuthentication.SetAuthCookie(result[0].UserName, false);
                    if (result[0].Isadmin == 1)
                    {
                        return RedirectToAction("Front", "Admin");
                    }
                    if (result[0].Isadmin == 2)
                    {
                        return RedirectToAction("Index", "User");
                    }

                }

                else
                {
                    ModelState.AddModelError("", "invalid username and password");
                }
            
           

            return View(model);
        }


        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using (var context = new TourismWebsiteDBEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMethod = "Registration Successfull";
            return RedirectToAction("Login");
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }   
}