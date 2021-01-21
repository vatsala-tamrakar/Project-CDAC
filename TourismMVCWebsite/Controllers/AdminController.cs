using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectDatabaseLib;

namespace TourismMVCWebsite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private TourismWebsiteDBEntities db = new TourismWebsiteDBEntities();

        public ActionResult Front()
        {
            return View();
        }
        // GET: Admin

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,Password,City,EmailId,ContactNumber,Isactive,Isadmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,Password,City,EmailId,ContactNumber,Isactive,Isadmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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

        //pakages operations
        public ActionResult Packages()
        {
            return View(db.Packages.ToList());
        }

        public ActionResult CreatePakages()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePakages(HttpPostedFileBase file, Package pkg)
        {
          
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/assets/images/"), _filename);
            pkg.Image = "~/Content/assets/images/" + _filename;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                if (file.ContentLength <= 10000000)
                    db.Packages.Add(pkg);
                if (db.SaveChanges() > 0)
                {
                    file.SaveAs(path);
                    ViewBag.msg = "Package Added";
                    ModelState.Clear();
                }

            }
            else
            {
                ViewBag.msg = "Size is not valid";
            }
            return View();
        }


        public ActionResult EditPakages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var packages = db.Packages.Find(id);
            Session["imgPath"] = packages.Image;
            if (packages == null)
            {
                return HttpNotFound();
            }

            return View(packages);
        }


        [HttpPost]
        public ActionResult EditPakages(HttpPostedFileBase file, Package pkg)
        {
            if (ModelState.IsValid)
            {
                if (file!= null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/images/"), _filename);
                    pkg.Image = "~/Content/assets/images/" + _filename;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        if (file.ContentLength <= 10000000)
                        {
                            db.Entry(pkg).State = EntityState.Modified;
                            string OldImgPath = Request.MapPath(Session["imgPath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                file.SaveAs(path);
                                if (System.IO.File.Exists(OldImgPath))
                                {
                                    System.IO.File.Delete(OldImgPath);
                                }
                                TempData["msg"] = "Record Updated";
                            }
                        }
                        else
                        {
                            ViewBag.msg = "Size is not valid";
                        }
                    } 
                }
            }
        
            else
            {
                pkg.Image = Session["imgPath"].ToString();
                db.Entry(pkg).State = EntityState.Modified;
                if (db.SaveChanges() > 0)
                {

                    TempData["msg"] = "Package Updated";
                    return RedirectToAction("Packages");
                }
            }
            return View();
        }




     
        [HttpPost]
        public ActionResult DeletePakages(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var packages = db.Packages.Find(id);
            Session["imgPath"] = packages.Image;
            if (packages == null)
            {
                return HttpNotFound();
            }
            string currentimg = Request.MapPath(packages.Image);
            db.Entry(packages).State = EntityState.Deleted;
            if (db.SaveChanges() > 0)
            {
                if(System.IO.File.Exists(currentimg))
                {
                    System.IO.File.Delete(currentimg);
                }
                TempData["msg"] = "Package Deleted";
                return RedirectToAction("Packages");
            }


            return View();
        }
    }
}
