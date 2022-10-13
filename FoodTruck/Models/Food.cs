using System.ComponentModel.DataAnnotations;

namespace FoodTruck.Models
{
    public class Food
    {
        [Key]
        public string applicant { get; set; }
        public string location { get; set; }
        public string start24 { get; set; }
        public string end24 { get; set; }
        public int dayorder { get; set; }


    }
}
