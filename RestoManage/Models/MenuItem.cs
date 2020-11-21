using System.Collections.Generic;

namespace RestoManage.Models
{
    /// <summary>
    /// Food Menu Item Model
    /// </summary>
    public class MenuItem
    {
        MenuItem()
        {
            UserFoodRating = new List<int>();
        }

        public int Id { get; set; }

        public string FoodName { get; set; }

        public float FoodRating { get; set; }

        public List<int> UserFoodRating { get; set; }

        public int Price { get; set; }

    }
}
