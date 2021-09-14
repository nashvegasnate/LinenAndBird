using LinenAndBird.DataAccess;
using LinenAndBird.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace LinenAndBird.Controllers
{
    [Route("api/birds")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        BirdRepository _repo;

        public BirdController()
        {
            _repo = new BirdRepository();
        }

        [HttpGet]
        public IActionResult GetAllBirds()
        {
            //Returns an "Ok" response
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetBirdById(Guid id)
        {

          var bird = _repo.GetById(id);

          if (bird is null)
          {
            return NotFound($"No bird with the id {id} was found.");
          }

          return Ok(bird);
        }

        [HttpPost]
        //IActionResult method - fancy way to return content & status codes together.
        //It is an interface which implements the BadRequest and Ok response
        public IActionResult AddBird(Bird newBird)
        {
            //Lets you know if you missed inputting a required property
            if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
            {
                return BadRequest("Name and Color are required fields");
            }

            _repo.Add(newBird);

            return Created("api/birds/1", newBird);
        }
    }
}
