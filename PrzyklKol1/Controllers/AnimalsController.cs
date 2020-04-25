using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzyklKol1.Services;

namespace PrzyklKol1.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {

        readonly AnimalsDBservice service;

        public AnimalsController(AnimalsDBservice service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult getAnimals(string orderBy)
        {

            if (orderBy == null)
                return BadRequest("brak orderBy");

            return Ok(service.getAnimals(orderBy));
        }


        [HttpPost]
        public IActionResult AddAnimal(Animal )
        {

        }

    }



}