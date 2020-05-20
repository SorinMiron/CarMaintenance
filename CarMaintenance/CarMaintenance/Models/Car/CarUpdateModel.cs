using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Car
{
    public class CarUpdateModel : CarBase<DateTime>
    {
        [Required]
        public int CarId { get; set; }
    }
    
}
