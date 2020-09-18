using DogGo.Models;
using System.Collections.Generic;

namespace DogGo.Controllers
{
    internal class DogFormViewModel
    {
        public Dog dog { get; set; }
        public List<Owner> Owners { get; set; }
    }
}