using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        //[Range(0,10)]
        public double Rating { get; set; }

        public bool IsRecommended => Rating > 3.5; // shorter formatting for get rating > 3.5 and if it is return true or false
        
    }
}