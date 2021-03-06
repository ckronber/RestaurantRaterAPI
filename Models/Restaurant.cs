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

        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();

        
        //[Range(0,10)]
        public double Rating {
            get 
            {
                double totalAverageRating = 0;
                foreach(Rating Rating in Ratings)
                {
                    totalAverageRating += Rating.AverageRating;
                }

                return totalAverageRating / Ratings.Count;
            }
        }

        //AverageFoodScore
        public double AverageFoodScore {
            get
            {
                double totalFoodRating = 0;
                foreach(Rating Rating in Ratings)
                {
                    totalFoodRating += Rating.FoodScore;
                }
                return totalFoodRating / Ratings.Count;
            } 
        }

        //AverageEnvironmentScore
        public double AverageEnvironmentScore
        {
            get
            {
                double totalEnvironmentScore = 0;

                foreach (Rating Rating in Ratings)
                {
                    totalEnvironmentScore += Rating.EnvironmentScore;
                }

                return totalEnvironmentScore / Ratings.Count;
            }
        }

        //AverageCleanlinessScore

        public double AverageCleanlinessScore
        {
            get
            {
                double totalCleanlinessScore = 0;

                foreach (Rating Rating in Ratings)
                {
                    totalCleanlinessScore+= Rating.CleanlinessScore;
                }

                return totalCleanlinessScore / Ratings.Count;
            }
        }

        public bool IsRecommended => Rating > 8.5; // shorter formatting for get rating > 3.5 and if it is return true or false
        
    }
}