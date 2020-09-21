using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogGo.Models.ViewModels
{
    public class WalkerProfileProfileViewModel
    {
        public Walker walker { get; set; }
        public List<Walk> Walks { get; set; }
    }
}
