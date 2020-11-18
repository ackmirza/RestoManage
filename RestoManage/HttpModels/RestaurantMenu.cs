using RestoManage.Models;

namespace RestoManage.HttpModels
{
    public class RestaurantMenu
    {
        public int RestaurantId { get; set; }

        private Menu _menu;
        public Menu Menu
        {
            get { return _menu; }
            set 
            { 
                _menu = value;
                UpdateFoods();
            }
        }

        private void UpdateFoods()
        {
            if(_menu != null && _menu.Foods != null)
            {
                int i = 0;
                _menu.Foods.ForEach(x =>
                {
                    x.Id = i+1;
                    i++;
                });
            }
        }
    }
}
