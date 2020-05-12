using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Car
{
    public class CarUpdateModel
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public int ActualKilometers { get; set; }
        [Required]
        public int LastRevisionKm { get; set; }
        [Required]
        public DateTime LastRevisionDate { get; set; }
        [Required]
        public DateTime LastPti { get; set; }
        [Required]
        public DateTime LastVig { get; set; }
        [Required]
        public DateTime LastInsurance { get; set; }
    }
    
}
