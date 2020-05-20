using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Car
{
    public class CarBase<T>
    {
        
        [Required]
        public int ActualKilometers { get; set; }
        [Required]
        public int LastRevisionKm { get; set; }
        [Required]
        public T LastRevisionDate { get; set; }
        [Required]
        public T LastPti { get; set; }
        [Required]
        public T LastVig { get; set; }
        [Required]
        public T LastInsurance { get; set; }
    }
}
