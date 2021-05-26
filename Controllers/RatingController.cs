using RestaurantRaterAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRaterAPI.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRating(Rating model)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(model.RestaurantId);

            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the Id of {model.RestaurantId} does not exist");
            }

            _context.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated {restaurant.Name} successfully");

            return InternalServerError();
        }

        //GetRatingsbyRestaurantID
        [HttpGet]
        public async Task<IHttpActionResult> GetRatingByRestaurantId(int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);

            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the Id of {Id} does not exist");
            }

            return Ok(restaurant.Ratings);
        }

        //UpdateRating
        [HttpPut]
        public async Task<IHttpActionResult> UpdateRatingByRestaurantId(int Id,Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Request");
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);
            Rating rating = await _context.Ratings.FindAsync(model.Id);

            if(restaurant == null)
            {
                return BadRequest("Restaurant Not Found");
            }

            if(rating.Id == model.Id)
            {
                rating.FoodScore = model.FoodScore;
                rating.EnvironmentScore = model.EnvironmentScore;
                rating.CleanlinessScore = model.CleanlinessScore;

                if (await _context.SaveChangesAsync() == 1)
                    return Ok($"You successfully updated a rating for {restaurant.Name}");
            }
         
            return InternalServerError();
        }
      
        //DeleteRating
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurantId(int Id,Rating model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid Request");
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(Id);

            if (restaurant == null)
            {
                return BadRequest("Restaurant Not Found");
            }

            Rating rating = await _context.Ratings.FindAsync(model.Id);
            rating.RestaurantId = restaurant.Id;
            
            _context.Ratings.Remove(rating);

            if (await _context.SaveChangesAsync() == 1)
                return Ok($"Rating #{rating.Id} for {restaurant.Name} was successfully deleted.");

            return InternalServerError();
        }
      
    }   
}
