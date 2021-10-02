using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Controllers
{
    //[Route("api/[controller]")] - this is the default when a new controller is created
    //OR--- YOU CAN DO IT LIKE THIS BELOW:

    [Route("api/hats")] //exposed at this endpoint
    [ApiController] //an api controller, so it returns json or xml
    public class HatsController : ControllerBase
    {
        IHatRepository _repo;

        //taking a dependency on an Interface rather than a concrete class has its advantages

        public HatsController(IHatRepository repo)
        {
            _repo = repo;
        }

        //Tells api how to expose this function.  
        [HttpGet]
        public List<Hat> GetAllHats()
        {
            return _repo.GetAll();
        }

        //This HttpGet adds below to the original directory of api/hats
        //GET /api/hats/styles/1 -> all open backed hats
        [HttpGet("styles/{style}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle style)
        {
            return _repo.GetByStyle(style);
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

