using ProjectDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourismMVCWebsite.Controllers
{
    public class BlogController : Controller
    {
        private TourismWebsiteDBEntities db = new TourismWebsiteDBEntities();
        // GET: Blog
        public ActionResult Blogs()
        {
            var blogdetails = db.BlogSpace1.ToList().OrderByDescending(x => x.Id);
            return View(blogdetails);
        }
        public ActionResult BlogLog()
        {
            var blogdetails = db.BlogSpace1.ToList().OrderByDescending(x => x.Id);
            return View(blogdetails);
        }

        [Authorize]
        public ActionResult Uploadblog()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Uploadblog(BlogSpace1 bg)
        {

            db.BlogSpace1.Add(bg);
            db.SaveChanges();
            ViewBag.message = "Blog details added successfully";
            return View();
        }

        public ActionResult Destination()
        {
            var destinationarticle = db.BlogSpace1.Where(x => x.Category == "Destination");
            return View(destinationarticle);
        }
        public ActionResult Travel_Experience()
        {
            var travelarticle = db.BlogSpace1.Where(x => x.Category == "Travel Experience");
            return View(travelarticle);

        }
        public ActionResult Miscellaneous()
        {
            var miscarticle = db.BlogSpace1.Where(x => x.Category == "Miscellaneous");
            return View(miscarticle);

        }
        public ActionResult shimla_destination()
        {
            return View();
        }
        public ActionResult food()
        {
            return View();
        }
        public ActionResult best_travel()
        {
            return View();
        }

    }
}