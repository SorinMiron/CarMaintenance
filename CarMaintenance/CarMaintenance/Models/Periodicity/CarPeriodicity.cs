using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using CarMaintenance.Models.Car;


namespace CarMaintenance.Models.Periodicity
{
    public class CarPeriodicity : CarPeriodicityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public CarPeriodicity() : base() { }

        public CarPeriodicity(int? revisionKm, int? revisionMonths, int? ptiMonths, int? vigMonths, int? insuranceMonths) : base(revisionKm, revisionMonths, ptiMonths, vigMonths, insuranceMonths)
        {
        }

        public CarPeriodicity(CarPeriodicityModel carPeriodicityModel) : base(carPeriodicityModel)
        {
        }
    }
}
