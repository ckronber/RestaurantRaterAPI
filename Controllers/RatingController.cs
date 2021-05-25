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
        public async Task<IHttpActionResult> GetRatingById(int RestaurantID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Restaurant restaurant = await _context.Restaurants.FindAsync(RestaurantID);

            if (restaurant == null)
            {
                return BadRequest($"The target restaurant with the Id of {RestaurantID} does not exist");
            }
            else
            {
                return Ok(restaurant.Ratings);
            }
            return InternalServerError();
        }
        //UpdateRating
        //DeleteRating
    }
}
