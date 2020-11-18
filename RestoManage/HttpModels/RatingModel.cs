namespace RestoManage.HttpModels
{
    public class RatingModel
    {
        public int RestaurantId { get; set; }

        public int FoodId { get; set; }

        public int? RestaurantRating { get; set; }

        public int? FoodRating { get; set; }
    }
}
