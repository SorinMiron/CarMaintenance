using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Car
{
    public class CarDetails 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int ActualKilometers { get; set; }
        [Required]
        public DateTime LastRevision { get; set; }
        [Required]
        public DateTime LastPti { get; set; }
        [Required]
        public DateTime LastVig { get; set; }

        public CarDetails()
        {

        }

        public CarDetails(string userId, CarDetailsModel carDetailsModel)
        {
            UserId = userId;
            Name = carDetailsModel.Name;
            Details = carDetailsModel.Details;
            Year = carDetailsModel.Year;
            ActualKilometers = carDetailsModel.ActualKilometers;
            LastRevision = new DateTime(carDetailsModel.LastRevision.Year, carDetailsModel.LastRevision.Month, carDetailsModel.LastRevision.Day);
            LastPti = new DateTime(carDetailsModel.LastPti.Year, carDetailsModel.LastPti.Month, carDetailsModel.LastPti.Day);
            LastVig = new DateTime(carDetailsModel.LastVig.Year, carDetailsModel.LastVig.Month, carDetailsModel.LastVig.Day); ;
        }
    }
}
