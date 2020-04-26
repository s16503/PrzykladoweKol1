using PrzyklKol1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PrzyklKol1.Requests
{
    public class AddAnimalRequest
    {
        [Required(ErrorMessage ="Pole wymagane")]
        public string  Name { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string AdmissionDate { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public int IdOwner { get; set; }


        public List<String> Procedures { get; set; }

    }
}
