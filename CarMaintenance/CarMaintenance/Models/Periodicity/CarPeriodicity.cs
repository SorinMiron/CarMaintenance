using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarMaintenance.Models.Car;


namespace CarMaintenance.Models.Periodicity
{
    public class CarPeriodicity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? RevisionKm { get; set; }
        public int? RevisionMonths { get; set; }
        public int? PtiMonths { get; set; }
        public int? VigMonths { get; set; }

        public CarPeriodicity() { }

        public CarPeriodicity(int? revisionKm, int? revisionMonths, int? ptiMonths, int? vigMonths)
        {
            RevisionKm = revisionKm;
            RevisionMonths = revisionMonths;
            PtiMonths = ptiMonths;
            VigMonths = vigMonths;
        }

        public CarPeriodicity(CarPeriodicityModel carPeriodicityModel)
        {
            RevisionKm = carPeriodicityModel.RevisionKm;
            RevisionMonths = carPeriodicityModel.RevisionMonths;
            PtiMonths = carPeriodicityModel.PtiMonths;
            VigMonths = carPeriodicityModel.VigMonths;
        }
    }
}
