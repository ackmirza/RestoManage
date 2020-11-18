using System.Collections.Generic;

namespace RestoManage.Models
{
    public class Food
    {
        Food()
        {
            UserRating = new List<int>();
        }

        public int Id { get; set; }

        public string FoodName { get; set; }

        public float Rating { get; set; }

        public List<int> UserRating { get; set; }

        public int Price { get; set; }

    }
}
