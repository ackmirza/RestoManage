using RestoManage.HttpModels;
using RestoManage.Managers;
using RestoManage.Models;
using System.Collections.Generic;

namespace RestoManage.Repositories
{
    public class RestaurantRepository
    {
        public bool AddRestaurant(Restaurant resto)
        {
            return DBManager.AddRestaurant(resto);
        }

        public bool AddOrUpdateMenu(RestaurantMenu menu)
        {
            return DBManager.AddOrUpdateRestaurantMenu(menu);
        }

        internal bool UpdateRestaurantOrFoodRating(RatingModel rating)
        {
            if(rating.RestaurantRating != null && rating.FoodRating== null)
            {
                return DBManager.UpdateRestaurantRating(rating.RestaurantId, rating.RestaurantRating);
            }
            else if(rating.FoodRating != null && rating.RestaurantRating == null)
            {
                return DBManager.UpdateFoodRating(rating.RestaurantId, rating.FoodId, rating.FoodRating);
            }

            return false;
        }

        internal List<Restaurant> SearchRestaurants(SearchRestaurantModel ratingFilter)
        {
            return DBManager.SearchRestaurants(ratingFilter);
        }
    }
}
