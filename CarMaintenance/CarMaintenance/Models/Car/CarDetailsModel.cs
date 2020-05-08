using System.ComponentModel.DataAnnotations;

namespace CarMaintenance.Models.Car
{
    public class CarDetailsModel
    {
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int ActualKilometers { get; set; }
        [Required]
        public Date LastRevision { get; set; }
        [Required]
        public Date LastPti { get; set; }
        [Required]
        public Date LastVig { get; set; }
        public CarDetailsModel()
        {

        }

        public CarDetailsModel(string name, string details, int year, int actualKilometers, Date lastRevisiom, Date lastPti, Date lastVig)
        {
            Name = name;
            Details = details;
            Year = year;
            ActualKilometers = actualKilometers;
            LastRevision = lastRevisiom;
            LastPti = lastPti;
            LastVig = lastVig;
        }

    }
}
