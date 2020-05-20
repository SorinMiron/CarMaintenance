using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Periodicity
{
    public class CarPeriodicityModel : CarPeriodicityBase
    {
        public string CarNameAndYear { get; set; }
        public int CarId { get; set; }

        public CarPeriodicityModel() : base()
        { }

        public CarPeriodicityModel(CarPeriodicityModel carPeriodicityModel) : base(carPeriodicityModel)
        {

        }

        public CarPeriodicityModel(int carId, string carNameAndYear, int? revisionKm, int? revisionMonths, int? ptiMonths, int? vigMonths, int? insuranceMonths)
        :base(revisionKm, revisionMonths, ptiMonths, vigMonths, insuranceMonths)
        {
            CarId = carId;
            CarNameAndYear = carNameAndYear;
        }
    }
}
