using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LinenAndBird.Controllers
{
    //[Route("api/[controller]")] - this is the default when a new controller is created
    //OR--- YOU CAN DO IT LIKE THIS BELOW:

    [Route("api/hats")] //exposed at this endpoint
    [ApiController] //an api controller, so it returns json or xml
    public class HatsController : ControllerBase
    {
        HatRepository _repo;

        public HatsController()
        {
            _repo = new HatRepository();
        }

        //Tells api how to expose this function.  
        [HttpGet]
        public List<Hat> GetAllHats()
        {
            return _repo.GetAll();
        }

        //This HttpGet adds below to the original directory of api/hats
        //GET /api/hats/styles/1 -> all open backed hats
        [HttpGet("styles/{styleOfHat}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle styleOfHat)
        {
            return _repo.GetByStyle(styleOfHat);
        }
        
        //Asking for someone to give a parameter of a hat - newHat.
        //It is an instance of the class Hat called newHat.
        //[HttpPost] - for adding item to a collection 
        [HttpPost]    
        public void AddAHat(Hat newHat)
        {
            _repo.Add(newHat);
        }
    }
}   

