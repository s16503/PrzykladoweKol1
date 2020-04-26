using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzyklKol1.Models;
using PrzyklKol1.Requests;
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
                return Ok(service.getAnimals("AdmissionDate DESC"));


            string[] arr = orderBy.Split(" ");
            if (arr.Length < 2)
                return BadRequest("ORDERBY: [columnName] [asc/desc]");

            if(!arr[1].ToUpper().Equals("ASC") && !arr[1].ToUpper().Equals("DESC"))
                return BadRequest("ORDERBY: [columnName] [asc/desc]");

          

            return Ok(service.getAnimals(orderBy));
        }


        [HttpPost]
        public IActionResult AddAnimal(AddAnimalRequest request)
        {
           
 Animal animal = service.AddAnimal(request);
                return Ok(animal);

            try
            {
               


            }catch(Exception ex)
            {
                
                return NotFound(ex.Message);
            }

           
        }

    }



}