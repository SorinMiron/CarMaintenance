using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CarMaintenance.Models.Periodicity;
namespace CarMaintenance.Models.Car
{
    public class CarDetails 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(450)")]
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
        public int PeriodicityId { get; set; }
        public CarPeriodicity Periodicity { get; set; }
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

        public CarDetails(string userId, CarDetailsModel carDetailsModel, CarPeriodicity periodicity) : this(userId, carDetailsModel)
        {
            Periodicity = periodicity;
        } 
    }
}
