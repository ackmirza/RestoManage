namespace RestoManage.HttpModels
{
    public class RatingModel
    {
        public int RestaurantId { get; set; }

        public int MenuItemId { get; set; }

        public int? RestaurantRating { get; set; }

        public int? MenuItemRating { get; set; }
    }
}
