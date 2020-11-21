using Microsoft.AspNetCore.Mvc;
using RestoManage.HttpModels;
using RestoManage.Models;
using RestoManage.Repositories;
using System.Collections.Generic;

namespace RestoManage.Controllers
{
    /// <summary>
    /// Restaurants Controller, it is Public API contoller
    /// All API calls travels through this.
    /// </summary>
    [Route("api")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        /// <summary>
        /// Default Get call which will return completed Assignment
        /// </summary>
        /// <returns>Returning Assignment detail as a message</returns>
        [HttpGet]
        public IActionResult Get()
        {
            string message = @"Welcome to Resto Manage API, I tried to implement below features

1>	Allow Restaurant owners to register their restaurants, registration details include Name, Address , Geo Location. Operation Timings, Contact Numbers.
2>	Allow Restaurant owners to publish their menu.
3>	Allow users to search for restaurants by menu items, name, address or geo location( within 5 kms radius) , operation timings.
4>	Allow users to provide ratings ( 1 to 5 ) to a menu item in a restaurant and overall rating (1 to 5 ) to a restaurant.
5>	Allow users to search or filter restaurants by item ratings and overall ratings.";
            return Ok(message);
        }


        /// <summary>
        /// New Restaurant Registration happens by this API
        /// </summary>
        /// <param name="resto">This will receive HTTP body data in Restaurant model</param>
        /// <returns>
        /// TODO: Suppose to return proper HTTP Response/Fault Model, 
        /// but as of now just returning API Status OK=200 or BadRequest=400
        /// </returns>
        [HttpPost("RegisterRestaurant")]
        public IActionResult CreateRestaurant(Restaurant resto)
        {
            var result = false;
            var error = string.Empty;
            RestaurantRepository repository = new RestaurantRepository();
            try
            {
                result = repository.AddRestaurant(resto);
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
            }

            if (result)
                return Ok();
            else
                return BadRequest(error);
        }

        /// <summary>
        /// API to recive Restaurant Menu from client
        /// </summary>
        /// <param name="menu">This will receive HTTP body data in Menu Item model</param>
        /// <returns>
        /// TODO: Suppose to return proper HTTP Response/Fault Model, 
        /// but as of now just returning API Status OK=200 or BadRequest=400
        /// </returns>
        [HttpPost("PublishMenu")]
        public IActionResult PublishRestaurantMenu(RestaurantMenu menu)
        {
            var result = false;
            var error = string.Empty;
            RestaurantRepository repository = new RestaurantRepository();
            try
            {
                result = repository.UpdateMenu(menu);
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
            }

            if (result)
                return Ok();
            else
                return BadRequest(error);
        }

        /// <summary>
        /// This API built to provide Rating for Restaurant or Menu Item
        /// 
        /// **** Request Body ****
        /// { 
        ///     "restaurantId": 0, 
        ///     "menuItemRating": 4, 
        ///     "menuItemId": 4 
        /// }
        /// **** Request Body ****
        /// </summary>
        /// <param name="rating">Provide Restaurant Rating or Food Rating</param>
        /// <returns>
        /// TODO: Suppose to return proper HTTP Response/Fault Model, 
        /// but as of now just returning API Status OK=200 or BadRequest=400
        /// </returns>
        [HttpPost("RestaurantRating")]
        public IActionResult RateTheRestaurant(RatingModel rating)
        {
            string fault = IsValidRating(rating);
            if (!string.IsNullOrEmpty(fault))
                return BadRequest(fault);

            var result = false;
            var error = string.Empty;

            RestaurantRepository repository = new RestaurantRepository();
            try
            {
                result = repository.UpdateRestaurantOrMenuItemRating(rating);
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
            }

            if (result)
                return Ok();
            else
                return BadRequest(error);
        }

        /// <summary>
        /// Filter Restaurants by given criteria
        /// - By Location (Lat/Long)
        /// - Food or Menu Item
        /// - Restaurant Name & Address
        /// - Sort by Food Popularity (rating)
        /// - Sort by Restaurant Popularity (rating)
        /// TODO: Operation Timing filter not applied
        /// </summary>
        /// <param name="ratingFilter">Query string parameters</param>
        /// <returns>
        /// Array of Filtered Restaurants
        /// TODO: Suppose to return proper HTTP Response/Fault Model, 
        /// but as of now just returning API Status OK=200 or BadRequest=400
        /// Status 200 with filtered data.
        /// </returns>
        [HttpGet("Search")]
        public IActionResult SearchRestaurants([FromQuery] SearchRestaurantModel ratingFilter)
        {
            if (!ratingFilter.IsNameExist &&
                !ratingFilter.IsMenuItemExist &&
                !ratingFilter.IsAddressExist &&
                !ratingFilter.IsLocationExist &&
                !ratingFilter.IsSortApplied)
                return BadRequest("Invalid search, please provide search criteria");

            List<Restaurant> result = null;
            var error = string.Empty;

            RestaurantRepository repository = new RestaurantRepository();
            try
            {
                result = repository.SearchRestaurants(ratingFilter);
            }
            catch (System.Exception ex)
            {
                error = ex.Message;
            }

            if (result != null)
                return Ok(result);
            else
                return BadRequest(error);
        }

        private string IsValidRating(RatingModel rating)
        {
            string error = null;

            if (rating != null)
            {
                if (!rating.MenuItemRating.HasValue && !rating.RestaurantRating.HasValue)
                    error = "Please check request body";
                else if (rating.MenuItemRating.HasValue && rating.MenuItemId <= 0)
                    error = "Invalid 'menuItemId'";
                else if (rating.RestaurantRating.HasValue && rating.RestaurantId < 0)
                    error = "Invalid 'restaurantId'";
                else if((rating.MenuItemRating.HasValue && !(rating.MenuItemRating >= 1 && rating.MenuItemRating <= 5)) ||
                        (rating.RestaurantRating.HasValue && !(rating.RestaurantRating >= 1 && rating.RestaurantRating <= 5)))
                    error = "Rating value should be in range of 1-5 number";
            }
            else
                error = "Body should not be empty";

            return error;
        }
    }
}
