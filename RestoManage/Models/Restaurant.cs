using System.Collections.Generic;

namespace RestoManage.Models
{
    /// <summary>
    /// Restaurant model class
    /// </summary>
    public class Restaurant
    {
        Restaurant()
        {
            UserRestaurantRating = new List<int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public float RestaurantRating { get; set; }

        public List<int> UserRestaurantRating { get; set; }

        public List<ContactNumber> ContactNos { get; set; }

        public List<OperationTime> OperationTimings { get; set; }

        public Menu Menu { get; set; }
    }

    public class ContactNumber
    {
        public string Title { get; set; }

        public string Phone { get; set; }
    }

    public class OperationTime
    {
        public string Day { get; set; }

        public double StartTime { get; set; }

        public double EndTime { get; set; }
    }    
}
