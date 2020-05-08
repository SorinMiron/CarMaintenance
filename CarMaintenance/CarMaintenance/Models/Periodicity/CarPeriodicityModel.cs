using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Periodicity
{
    public class CarPeriodicityModel
    {
        public string CarNameAndYear { get; set; }
        public int CarId { get; set; }
        public int? RevisionKm { get; set; }
        public int? RevisionMonths { get; set; }
        public int? PtiMonths { get; set; }
        public int? VigMonths { get; set; }

        public CarPeriodicityModel()
        {

        }

        public CarPeriodicityModel(int carId, string carNameAndYear, int? revisionKm, int? revisionMonths, int? ptiMonths, int? vigMonths)
        {
            CarId = carId;
            CarNameAndYear = carNameAndYear;
            RevisionKm = revisionKm;
            RevisionMonths = revisionMonths;
            PtiMonths = ptiMonths;
            VigMonths = vigMonths;
        }
    }
}
