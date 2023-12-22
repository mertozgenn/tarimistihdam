using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.Rating
{
    public class EmployerRatingDto: EmployerRating
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
