using ProjectDb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessl
{
    public interface DataAccessInterface
    {
        //location
        //user
        List<double> GetCoordinates(string location);
        //admin
        Task<bool> InsertLocation(Locations obj);
        bool UpdateLocation(Locations obj);
        bool DeleteLocation(int id);
        List<Locations> GetAllLocations();

        //Blogspace
        //user
        List<BlogSpace> GetBlogs(String title);
        List<BlogSpace> GetBlogsByLocation(String Location);
        bool CreateBlog(BlogSpace UserBlog);
        bool UpdateBlog(BlogSpace obj);

        //admin
        bool DeleteBlog(int id);
        List<BlogSpace> GetAllBlogs();

        //Hotels:
        //User
        List<Hotels> GetHotelsByName(string Name);
        // List<Hotels> GetHotelsByLocation(string Location);

        List<Hotels> GetHotelsByLocation(int Locationid);
        //double GetHotelPrice(string Name);
        Hotels GetHotelPrice(int id);

        //admin
        bool InsertHotel(Hotels obj);
        bool UpdateHotel(Hotels obj);
        bool DeleteHotel(int id);
        List<Hotels> GetAllHotel();

        //TouristPlaces:
        //User
        List<TouristPlaces> GetPlacesByLocation(int Locationid);
        List<TouristPlaces> GetPlacesByName(string Name);
        List<TouristPlaces> GetPlacesByRating(int Rating);
        //Admin
        bool InsertTPlace(TouristPlaces obj);
        bool UpdatePlace(TouristPlaces obj);
        bool DeletePlace(int id);
        List<TouristPlaces> GetAllPlace();

        //Usertable
        //user
        bool CreateUser(User obj);
        //admin
        bool UpdateUser(User obj);
        bool DeleteUser(int id);
        List<User> GetAllUsers();

        //User Review
        //user
        bool RatingAPlace(UserReview Review);
        //admin
        bool DeleteReview(int id);
        List<UserReview> GetUserReviews();
    }
}
