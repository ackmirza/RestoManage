using System.Collections.Generic;

namespace RestoManage.HttpModels
{
    public class SearchRestaurantModel
    {
        public double Lat { get; set; }

        public double Long { get; set; }

        public int SearchDistance { get; set; } = 5; //In KM

        public string Food { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<string> Sort { get; set; }

        public bool IsLocationExist => (Lat != 0 && Long != 0);

        public bool IsNameExist => Name != null;

        public bool IsFoodExist => Food != null;

        public bool IsAddressExist => Address != null;

        public bool IsSortApplied => Sort != null && Sort.Count > 0;

    }
}
