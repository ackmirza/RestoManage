using Microsoft.AspNetCore.Mvc;
using RestoManage.HttpModels;
using RestoManage.Models;
using RestoManage.Repositories;

namespace RestoManage.Controllers
{
    [Route("api")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
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


        [HttpPost("RegisterRestaurant")]
        public IActionResult CreateRestaurant(Restaurant resto)
        {
            RestaurantRepository repository = new RestaurantRepository();
            var result = repository.AddRestaurant(resto);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("PublishMenu")]
        public IActionResult PublishRestaurantMenu(RestaurantMenu menu)
        {
            RestaurantRepository repository = new RestaurantRepository();
            var result = repository.AddOrUpdateMenu(menu);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("RestaurantRating")]
        public IActionResult RateTheRestaurant(RatingModel rating)
        {
            RestaurantRepository repository = new RestaurantRepository();
            var result = repository.UpdateRestaurantOrFoodRating(rating);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet("Search")]
        public IActionResult SearchRestaurants([FromQuery] SearchRestaurantModel ratingFilter)
        {
            if (!ratingFilter.IsNameExist && 
                !ratingFilter.IsFoodExist && 
                !ratingFilter.IsAddressExist &&
                !ratingFilter.IsLocationExist &&
                !ratingFilter.IsSortApplied)
                return BadRequest("Invalid search, please provide search criteria");

            RestaurantRepository repository = new RestaurantRepository();
            var result = repository.SearchRestaurants(ratingFilter);

            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
