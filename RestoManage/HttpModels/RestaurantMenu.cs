using RestoManage.Models;

namespace RestoManage.HttpModels
{
    /// <summary>
    /// Restaurant Menu
    /// </summary>
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
                GenerateMenuItemIds();
            }
        }

        private void GenerateMenuItemIds()
        {
            if(_menu != null && _menu.MenuItems != null)
            {
                int i = 0;
                _menu.MenuItems.ForEach(x =>
                {
                    x.Id = i+1;
                    i++;
                });
            }
        }
    }
}
