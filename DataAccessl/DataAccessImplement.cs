using ProjectDb;
using ProjectDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessl
{
    public class DataAccessImplement : DataAccessInterface
    {
        Context dbcon;
        public DataAccessImplement()
        {
            dbcon = new Context();
        }
        public bool CreateBlog(BlogSpace UserBlog)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.BlogSpaces.Add(UserBlog);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }


            return status;
        }

        public bool CreateUser(User obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.User.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool DeleteBlog(int id)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var Todel = dbcon.BlogSpaces.Find(id);
                dbcon.BlogSpaces.Remove(Todel);
                int change = dbcon.SaveChanges();

                if (change > 0)
                    status = true;
            }
            return status;

        }

        public bool DeleteHotel(int id)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var Todel = dbcon.Hotels.Find(id);
                dbcon.Hotels.Remove(Todel);
                int change = dbcon.SaveChanges();

                if (change > 0)
                    status = true;
            }
            return status;
        }

        public bool DeleteLocation(int id)
        {
            bool status = false;
            var Todel = dbcon.Locations.Find(id);
            dbcon.Locations.Remove(Todel);
            int change = dbcon.SaveChanges();
            dbcon.Dispose();
            if (change > 0)
                status = true;
            return status;
        }

        public bool DeletePlace(int id)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var Todel = dbcon.TouristPlaces.Find(id);
                dbcon.TouristPlaces.Remove(Todel);
                int change = dbcon.SaveChanges();

                if (change > 0)
                    status = true;
            }
            return status;
        }

        public bool DeleteReview(int id)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var Todel = dbcon.UserReview.Find(id);
                dbcon.UserReview.Remove(Todel);
                int change = dbcon.SaveChanges();
                if (change > 0)
                    status = true;
            }
            return status;
        }

        public bool DeleteUser(int id)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var Todel = dbcon.User.Find(id);
                dbcon.User.Remove(Todel);
                int change = dbcon.SaveChanges();
                if (change > 0)
                    status = true;
            }
            return status;
        }

        public List<BlogSpace> GetAllBlogs()
        {
            List<BlogSpace> Bloglist = new List<BlogSpace>();
            using (dbcon = new Context())
            {
                Bloglist = dbcon.BlogSpaces.ToList();

            }

            return Bloglist;
        }

        public List<Hotels> GetAllHotel()
        {

            List<Hotels> Hotellist = new List<Hotels>();
            using (dbcon = new Context())
            {
                Hotellist = dbcon.Hotels.ToList();

            }

            return Hotellist;
        }

        public List<Locations> GetAllLocations()
        {
            List<Locations> LocList = new List<Locations>();
            using (dbcon = new Context())
            {

                LocList = dbcon.Locations.ToList();



            }

            return LocList;
        }

        public List<TouristPlaces> GetAllPlace()
        {
            List<TouristPlaces> TPlaceList = new List<TouristPlaces>();
            using (dbcon = new Context())
            {
                TPlaceList = dbcon.TouristPlaces.ToList();

            }

            return TPlaceList;
        }

        public List<User> GetAllUsers()
        {
            List<User> UserList = new List<User>();
            using (dbcon = new Context())
            {
                UserList = dbcon.User.ToList();

            }

            return UserList;
        }

        public List<BlogSpace> GetBlogs(string title)
        {
            List<BlogSpace> BlogList = new List<BlogSpace>();
            using (dbcon = new Context())
            {
                BlogList = dbcon.BlogSpaces.Where(x => x.Title == title).ToList();


            }

            return BlogList;
        }

        public List<BlogSpace> GetBlogsByLocation(string location)
        {
            List<BlogSpace> BlogList = new List<BlogSpace>();
            using (dbcon = new Context())
            {
                BlogList = dbcon.BlogSpaces.Where(x => (string)x.LocationName == location).ToList();

            }

            return BlogList;
        }


        public List<double> GetCoordinates(string location)
        {
            List<double> coords = new List<double>();
            using (dbcon = new Context())
            {
                var req = dbcon.Locations.Where(x => x.LocationName == location).FirstOrDefault();
                coords.Add(req.Latitude);
                coords.Add(req.Longitude);

            }
            return coords;
        }



        public Hotels GetHotelPrice(int id)
        {
            Hotels HotelPrice = null;
            using (dbcon = new Context())
            {
                HotelPrice = (Hotels)dbcon.Hotels.Where(x => x.Id == id).First();

            }

            return HotelPrice;

        }

        public List<Hotels> GetHotelsByLocation(int Locationid)
        {
            List<Hotels> HotelList = new List<Hotels>();
            using (dbcon = new Context())
            {
                HotelList = dbcon.Hotels.Where(x => (int)x.LocationId == Locationid).ToList();

            }

            return HotelList;
        }

        public List<Hotels> GetHotelsByName(string Name)
        {
            List<Hotels> HotelsListByName = new List<Hotels>();
            using (dbcon = new Context())
            {
                HotelsListByName = dbcon.Hotels.Where(x => (string)x.HotelName == Name).ToList();

            }

            return HotelsListByName;
        }

        public List<TouristPlaces> GetPlacesByLocation(int Locationid)
        {
            List<TouristPlaces> PlacesListbyLocation = new List<TouristPlaces>();
            using (dbcon = new Context())
            {
                PlacesListbyLocation = dbcon.TouristPlaces.Where(x => (int)x.LocationId == Locationid).ToList();

            }

            return PlacesListbyLocation;
        }

        public List<TouristPlaces> GetPlacesByName(string Name)
        {
            List<TouristPlaces> PlacesListbyName = new List<TouristPlaces>();
            using (dbcon = new Context())
            {
                PlacesListbyName = dbcon.TouristPlaces.Where(x => (string)x.PlaceName == Name).ToList();

            }

            return PlacesListbyName;
        }

        public List<TouristPlaces> GetPlacesByRating(int Rating)
        {
            List<TouristPlaces> PlacesByRating = new List<TouristPlaces>();
            using (dbcon = new Context())
            {
                PlacesByRating = dbcon.TouristPlaces.Where(x => (int)x.Ratings == Rating).ToList();

            }

            return PlacesByRating;
        }

        public List<UserReview> GetUserReviews()
        {

            List<UserReview> UList = new List<UserReview>();
            return UList;
        }

        public bool InsertHotel(Hotels obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.Hotels.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public async Task<bool> InsertLocation(Locations obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.Locations.Add(obj);
                int changes = await dbcon.SaveChangesAsync();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool InsertTPlace(TouristPlaces obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.TouristPlaces.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool RatingAPlace(UserReview Review)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.UserReview.Add(Review);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool UpdateBlog(BlogSpace obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.BlogSpaces.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool UpdateHotel(Hotels obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.Hotels.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool UpdateLocation(Locations obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                dbcon.Locations.Add(obj);
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }
            return status;
        }

        public bool UpdatePlace(TouristPlaces obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var uobj = dbcon.TouristPlaces.Where(x => x.Id == obj.Id).First();
                uobj.PlaceName = obj.PlaceName;
                uobj.Ratings = obj.Ratings;

                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }

            return status;
        }

        public bool UpdateUser(User obj)
        {
            bool status = false;
            using (dbcon = new Context())
            {
                var uobj = dbcon.User.Where(x => x.UserId == obj.UserId).First();
                uobj.UserName = obj.UserName;
                uobj.Isactive = obj.Isactive;
                uobj.ContactNumber = obj.ContactNumber;
                int changes = dbcon.SaveChanges();
                if (changes > 0)
                {
                    status = true;
                }
            }

            return status;
        }
    }
}
