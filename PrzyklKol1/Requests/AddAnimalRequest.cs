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
        [Required(ErrorMessage ="Animal wymagane")]
        public Animal animal { get; set; }

        public List<String> Procedures { get; set; }


        //[Required(ErrorMessage ="pole wymagane")]
        //public String Name { get; set; }

        //[Required(ErrorMessage = "pole wymagane")]
        //public String Type { get; set; }

        //[Required(ErrorMessage = "pole wymagane")]
        //public String AdmDate { get; set; }

        //[Required(ErrorMessage = "pole wymagane")]
        //public String OwnerName { get; set; }
    }
}
