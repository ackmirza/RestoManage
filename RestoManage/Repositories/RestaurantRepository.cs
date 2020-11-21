using RestoManage.HttpModels;
using RestoManage.Managers;
using RestoManage.Models;
using System.Collections.Generic;

namespace RestoManage.Repositories
{
    /// <summary>
    /// Implemented Repository pattern to build data flow pipeline 
    /// between data and controller. It helps to segregate functionaly according to data
    /// </summary>
    public class RestaurantRepository
    {
        public bool AddRestaurant(Restaurant resto)
        {
            return DBManager.AddRestaurant(resto);
        }

        public bool UpdateMenu(RestaurantMenu menu)
        {
            return DBManager.AddOrUpdateRestaurantMenu(menu);
        }

        internal bool UpdateRestaurantOrMenuItemRating(RatingModel rating)
        {
            if(rating.RestaurantRating != null && rating.MenuItemRating== null)
            {
                return DBManager.UpdateRestaurantRating(rating.RestaurantId, rating.RestaurantRating);
            }
            else if(rating.MenuItemRating != null && rating.RestaurantRating == null)
            {
                return DBManager.UpdateMenuItemRating(rating.RestaurantId, rating.MenuItemId, rating.MenuItemRating);
            }

            return false;
        }

        internal List<Restaurant> SearchRestaurants(SearchRestaurantModel ratingFilter)
        {
            return DBManager.SearchRestaurants(ratingFilter);
        }
    }
}
