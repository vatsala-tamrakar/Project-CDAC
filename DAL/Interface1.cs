using ProjectDatabaseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface DataAccessInterface
    {





        //Flights
        Flight GetFlightName(int F_Id);
        List<Flight> GetAllFlights();

        List<Flight> GetFlightsbyLocation(int LocationId);

        List<Flight> GetFlightbyDate(string Departure_Date);

        List<Flight> GetFlightbyTime(string Departure_Time);

        bool InsertFlight(Flight obj);
        bool UpdateFlight(Flight obj);
        bool DeleteFlight(int F_Id);

        //TouristPlaces:
        //User

    }
    }
