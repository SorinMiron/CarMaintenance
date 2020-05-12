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
        public int LastRevisionKm { get; set; }
        [Required]
        public Date LastRevisionDate { get; set; }
        [Required]
        public Date LastPti { get; set; }
        [Required]
        public Date LastVig { get; set; }
        [Required]
        public Date LastInsurance { get; set; }
        public CarDetailsModel()
        {

        }

        public CarDetailsModel(string name, string details, int year, int actualKilometers, int lastRevisionKm, Date lastRevisionDate, Date lastPti, Date lastVig, Date lastInsurance)
        {
            Name = name;
            Details = details;
            Year = year;
            ActualKilometers = actualKilometers;
            LastRevisionKm = lastRevisionKm;
            LastRevisionDate = lastRevisionDate;
            LastPti = lastPti;
            LastVig = lastVig;
            LastInsurance = lastInsurance;
        }

    }
}
