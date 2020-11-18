using System.Collections.Generic;

namespace RestoManage.Models
{
    public class Restaurant
    {
        Restaurant()
        {
            UserRating = new List<int>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Lat { get; set; }

        public double Long { get; set; }

        public float Rating { get; set; }

        public List<int> UserRating { get; set; }

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
