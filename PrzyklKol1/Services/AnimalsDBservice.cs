using PrzyklKol1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzyklKol1.Services
{

    public interface AnimalsDBservice
    {
        public IEnumerable<Animal> getAnimals(string columnName);
    }
}
