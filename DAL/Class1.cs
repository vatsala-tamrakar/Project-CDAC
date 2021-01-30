
using ProjectDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DAL
{
    public class DataAccessImplement : DataAccessInterface
    {
        TourismWebsiteDBEntities dbcon;
        public DataAccessImplement()
        {
            dbcon = new TourismWebsiteDBEntities();
        }
      
        public bool InsertFlight(Flight obj)
        {
            bool status = false;
            using (dbcon = new TourismWebsiteDBEntities())
            {
                dbcon.Flights.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }


        public bool DeleteFlight(int F_Id)
        {
            bool status = false;
            using (dbcon = new TourismWebsiteDBEntities())
            {
                var Todel = dbcon.Flights.Find(F_Id);
                dbcon.Flights.Remove(Todel);
                int change = dbcon.SaveChanges();
                if (change > 0)
                    status = true;
            }
            return status;
        }
       

       






    
      
        public List<Flight> GetAllFlights()
        {
            List<Flight> Flightlist = new List<Flight>();
            using (dbcon = new TourismWebsiteDBEntities())
            {
                Flightlist = dbcon.Flights.ToList();

            }

            return Flightlist;
        }

    

      

      

        





        public List<Flight> GetFlightbyDate(string Departure_Date)
        {
            List<Flight> FlightList = new List<Flight>();
            using (dbcon = new TourismWebsiteDBEntities())
            {
                FlightList = dbcon.Flights.Where(x => x.Departure_Date == Departure_Date).ToList();

            }

            return FlightList;
        }

        public List<Flight> GetFlightbyTime(string Departure_Time)
        {
            List<Flight> FlightList = new List<Flight>();
            using (dbcon = new TourismWebsiteDBEntities())
            {
                FlightList = dbcon.Flights.Where(x => x.Departure_Time == Departure_Time).ToList();

            }

            return FlightList;
        }

        public Flight GetFlightName(string F_Name)
        {
            Flight flight = null;
            using (dbcon = new TourismWebsiteDBEntities())
            {
                flight = (Flight)dbcon.Flights.Where(x => x.F_Name == F_Name).First();

            }

            return flight;
        }

        public List<Flight> GetFlightsbyLocation(int LocationId)
        {
            List<Flight> FlightList = new List<Flight>();
            using (dbcon = new TourismWebsiteDBEntities())
            {
                FlightList = dbcon.Flights.Where(x => (int)x.LocationId == LocationId).ToList();

            }

            return FlightList;
        }

      

     
        

      


        

      


        public bool UpdateFlight(Flight obj)
        {
            bool status = false;
            using (dbcon = new TourismWebsiteDBEntities())
            {
                var flight = dbcon.Flights.Find(obj.F_Id);
                flight.F_Name = obj.F_Name;
                flight.Start_From = obj.Start_From;
                flight.Destination = obj.Destination;
                flight.Departure_Date = obj.Departure_Date;
                flight.Departure_Time = obj.Departure_Time;
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

       

        public Flight GetFlightName(int F_Id)
        {
            Flight F_Name = new Flight();
            using (dbcon = new TourismWebsiteDBEntities())
            {
                F_Name = (Flight)dbcon.Flights.Where(x => x.F_Id == F_Id).First();

            }

            return F_Name;
        }

       

 
       


       

      

     

     

      

      
    }
}

