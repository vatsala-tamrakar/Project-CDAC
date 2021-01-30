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
using Testdal;
using DAL;

namespace TourismMVCWebsite.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private TourismWebsiteDBEntities db = new TourismWebsiteDBEntities();
        private readonly DalInter con;// kushal
        private readonly DataAccessInterface dalobj; //aishwarya

        public AdminController()
        {
            con = new DalImp();
            dalobj = new DataAccessImplement();

        }
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

        //public Package GetPackagename(string P_Name)
        //{
        //    Package pkg = null;

        //    pkg = (Package)db.Packages.Where(x => x.PlaceName == P_Name).First();

       
        //    return pkg;
        //}




        //Hotel 
        public ActionResult Hotels(string Id)
        {


            if (Id == null)
            {
                Id = "";
            }
            IEnumerable<Hotel> ListHotels = con.GetAllHotel();



            // IEnumerable<Product> products = repo.GetProducts();
            if (!string.IsNullOrEmpty(Id) && Id != "All")
            {
                ListHotels = ListHotels.Where(p => p.HotelName.ToLower() == Id.ToLower());
            }
            return View(ListHotels);
        }
        public ActionResult NewHotel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewHotel(HttpPostedFileBase file, Hotel obj)
        {
            string filename = Path.GetFileName(file.FileName);
            string _filename = DateTime.Now.ToString("yymmssfff") + filename;
            string extension = Path.GetExtension(file.FileName);
            string path = Path.Combine(Server.MapPath("~/Content/assets/images/"), _filename);
            obj.hotelImage = "~/Content/assets/images/" + _filename;
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
            {
                bool changes = false;
                if (file.ContentLength <= 10000000)
                {
                    changes = con.InsertHotel(obj);
                }
                if (changes == true)
                {
                    file.SaveAs(path);
                    ViewBag.msg = "Hotel Added";
                    ModelState.Clear();
                }

            }
            else
            {
                ViewBag.msg = "Size is not valid";
            }
            return View();
        }

        public ActionResult HotelUpdate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = con.GetHotel((int)id);
            Session["oldImage"] = hotel.hotelImage;
            return View(hotel);
        }
        [HttpPost]
        public ActionResult HotelUpdate(HttpPostedFileBase file, Hotel obj)
        {
            
                if (file != null)
                {
                    string filename = Path.GetFileName(file.FileName);
                    string _filename = DateTime.Now.ToString("yymmssfff") + filename;
                    string extension = Path.GetExtension(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/assets/images/"), _filename);
                    obj.hotelImage = "~/Content/assets/images/" + _filename;
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png")
                    {
                        bool changes = false;
                        if (file.ContentLength <= 10000000)
                        {
                            changes = con.UpdateHotel(obj);
                            string OldImage = Request.MapPath(Session["oldImage"].ToString());
                            if (changes == true)
                            {
                                file.SaveAs(path);
                                if (OldImage != obj.hotelImage.ToString())
                                {
                                    if (System.IO.File.Exists(OldImage))
                                    {
                                        System.IO.File.Delete(OldImage);
                                    }
                                }
                                /*ViewBag.msg = "Hotel Updated";
                                ModelState.Clear();*/
                            }
                        }

                        else
                        {
                            ViewBag.msg = "Size is not valid";
                        }
                    }
                }
            
            else
            {
                obj.hotelImage = Session["oldImage"].ToString();
                if (con.UpdateHotel(obj))
                {
                    return RedirectToAction("Hotels");
                }

            }

            return View();
        }
        public ActionResult HotelDelete(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Hotels");
            }

            Hotel hotel = con.DeleteHotel((int)id);
            string Oldimage = Request.MapPath(hotel.hotelImage.ToString());

            if (System.IO.File.Exists(Oldimage))
            {
                System.IO.File.Delete(Oldimage);
            }

            return RedirectToAction("Hotels");
        }



        //Flight 
        public ActionResult NewFlight()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewFlight(Flight obj)
        {
            bool changes = false;
            changes = dalobj.InsertFlight(obj);

            if (changes == true)
            {

                ViewBag.msg = "Flight Details Added";
                ModelState.Clear();
            }
            else
            {
                ViewBag.msg = "Flight Details Not Added";
            }
            return View();
        }

        public ActionResult FlightUpdate(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flight flight = dalobj.GetFlightName((int)Id);

            return View(flight);
        }

        public ActionResult FlightDelete(int? F_Id)
        {

            if (F_Id == null)
            {
                return RedirectToAction("Flights");
            }

            dalobj.DeleteFlight((int)F_Id);

            return RedirectToAction("Flights");
        }
        public ActionResult Flights(string F_Id)
        {


            if (F_Id == null)
            {
                F_Id = "";
            }
            IEnumerable<Flight> ListFlights = dalobj.GetAllFlights();



            // IEnumerable<Product> products = repo.GetProducts();
            if (!string.IsNullOrEmpty(F_Id) && F_Id != "All")
            {
                ListFlights = ListFlights.Where(p => p.F_Name.ToLower() == F_Id.ToLower()).OrderByDescending(x => x.F_Id);
            }
            return View(ListFlights);
        }

        [HttpPost]
        public ActionResult FlightUpdate(Flight obj)
        {
            bool changes = false;
            changes = dalobj.UpdateFlight(obj);




            if (dalobj.UpdateFlight(obj))
            {
                return RedirectToAction("Flights");
            }



            return View();
        }
    }
}
