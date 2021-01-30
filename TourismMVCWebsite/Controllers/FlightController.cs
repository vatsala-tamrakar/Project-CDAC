using DAL;
using ProjectDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourismMVCWebsite.Controllers
{
    public class FlightController : Controller
    {
        // GET: Flight

        private readonly DataAccessInterface con;
        public FlightController()
        {
            con = new DataAccessImplement();
        }
        public ActionResult Index(string Id)
        {
            if (Id == null)
            {
                Id = "";
            }
            IEnumerable<Flight> Lflights = con.GetAllFlights();


            if (!string.IsNullOrEmpty(Id) && Id != "All")
            {
                Lflights = Lflights.Where(p => p.F_Name.ToLower() == Id.ToLower());
            }
            return View(Lflights);

        }

        public ActionResult UserIndex(string Id)
        {
            if (Id == null)
            {
                Id = "";
            }
            IEnumerable<Flight> Lflights = con.GetAllFlights();


            if (!string.IsNullOrEmpty(Id) && Id != "All")
            {
                Lflights = Lflights.Where(p => p.F_Name.ToLower() == Id.ToLower());
            }
            return View(Lflights);

        }
    }
}