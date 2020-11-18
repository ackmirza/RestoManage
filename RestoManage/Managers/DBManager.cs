using JsonFlatFileDataStore;
using RestoManage.Constants;
using RestoManage.HttpModels;
using RestoManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestoManage.Managers
{
    public class DBManager
    {
        static DataStore restaurantObj = new DataStore("data.json");
        
        static IDocumentCollection<Restaurant> restaurantCollection = restaurantObj.GetCollection<Restaurant>();

        internal static bool AddRestaurant(Restaurant resto)
        {
            var restaurant = GetRestaurantByName(resto.Name, resto.Lat, resto.Long);
            if (restaurant == null)
                return restaurantCollection.InsertOne(resto);
            else
                return false;
        }

        internal static Restaurant GetRestaurantByName(string restoName, double lat, double lng)
        {
            return restaurantCollection.AsQueryable()
                .FirstOrDefault(x => x.Name.ToLower().Trim().Equals(restoName) && x.Lat == lat && x.Long == lng);
        }

        internal static Restaurant GetRestaurantById(int restoId)
        {
            return restaurantCollection.AsQueryable()
                .FirstOrDefault(x => x.Id == restoId);
        }

        internal static bool AddOrUpdateRestaurantMenu(RestaurantMenu menu)
        {
            var restaurant = GetRestaurantById(menu.RestaurantId);
            if (restaurant != null)
            {
                restaurant.Menu = menu.Menu;
                return restaurantCollection.UpdateOne(menu.RestaurantId, restaurant);
            }
            else
                return false;
        }

        internal static bool UpdateRestaurantRating(int restaurantId, int? restaurantRating)
        {
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant != null)
            {
                restaurant.UserRating.Add((int)restaurantRating);
                restaurant.Rating = (float)restaurant.UserRating.Average();

                return restaurantCollection.UpdateOne(restaurantId, restaurant);
            }
            
            return false;
        }

        internal static bool UpdateFoodRating(int restaurantId, int foodId, int? foodRating)
        {
            bool result = false;
            var restaurant = GetRestaurantById(restaurantId);
            if (restaurant != null && restaurant.Menu != null && restaurant.Menu.Foods.Count > 0)
            {
                var food = restaurant.Menu.Foods.FirstOrDefault(x => x.Id == foodId);
                if (food != null)
                {
                    food.UserRating.Add((int)foodRating);
                    food.Rating = (float)food.UserRating.Average();

                    result = restaurantCollection.UpdateOne(restaurantId, restaurant);
                }
            }

            return result;
        }

        internal static List<Restaurant> SearchRestaurants(SearchRestaurantModel searchFilter)
        {
            IEnumerable<Restaurant> query = restaurantCollection.AsQueryable();
            
            if (searchFilter.IsLocationExist)
            {
                query = GetAllRestaurantsNearMe(searchFilter.Lat, searchFilter.Long, searchFilter.SearchDistance);
            }
            if (searchFilter.IsNameExist)
            {
                query = query.Where(x => x.Name.Contains(searchFilter.Name));
            }
            if (searchFilter.IsAddressExist)
            {
                query = query.Where(x => x.Address.Contains(searchFilter.Address));
            }
            if (searchFilter.IsFoodExist)
            {
                query = query.Where(x => x.Menu.Foods.Any(y => y.FoodName.Contains(searchFilter.Food)));
            }
            if (searchFilter.IsSortApplied)
            {
                searchFilter.Sort.ForEach(filter =>
                {
                    switch (filter)
                    {
                        case Sort.FoodRating:
                            query = query.OrderBy(x => x.Menu.Foods.Average(y => y.Rating));
                            break;
                        case Sort.RestaurantRating:
                            query = query.OrderByDescending(x => x.Rating);
                            break;
                        default:
                            break;
                    }
                });
            }
            
            return query.ToList();
        }

        internal static List<Restaurant> GetAllRestaurantsNearMe(double lat, double lng, int radiusInKm)
        {
            var nearestLocations = restaurantCollection.AsQueryable().Where(x => Distance(lat, lng, x.Lat, x.Long) < radiusInKm).ToList();
            return nearestLocations;
        }

        private static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(Deg2rad(lat1)) * Math.Sin(Deg2rad(lat2)) + Math.Cos(Deg2rad(lat1)) * Math.Cos(Deg2rad(lat2)) * Math.Cos(Deg2rad(theta));
            dist = Math.Acos(dist);
            dist = Rad2deg(dist);
            dist = (dist * 60 * 1.1515) / 0.6213711922;          //miles to kms
            return (dist);
        }

        private static double Deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double Rad2deg(double rad)
        {
            return (rad * 180.0 / Math.PI);
        }
    }
}
