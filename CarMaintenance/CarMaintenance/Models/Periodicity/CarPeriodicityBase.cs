using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.Periodicity
{
    public class CarPeriodicityBase
    {
        public int? RevisionKm { get; set; }
        public int? RevisionMonths { get; set; }
        public int? PtiMonths { get; set; }
        public int? VigMonths { get; set; }
        public int? InsuranceMonths { get; set; }

        public CarPeriodicityBase()
        {

        }
        public CarPeriodicityBase(int? revisionKm, int? revisionMonths, int? ptiMonths, int? vigMonths, int? insuranceMonths)
        {
            RevisionKm = revisionKm;
            RevisionMonths = revisionMonths;
            PtiMonths = ptiMonths;
            VigMonths = vigMonths;
            InsuranceMonths = insuranceMonths;
        }

        public CarPeriodicityBase(CarPeriodicityModel carPeriodicityModel)
        {
            RevisionKm = carPeriodicityModel.RevisionKm;
            RevisionMonths = carPeriodicityModel.RevisionMonths;
            PtiMonths = carPeriodicityModel.PtiMonths;
            VigMonths = carPeriodicityModel.VigMonths;
            InsuranceMonths = carPeriodicityModel.InsuranceMonths;
        }

    }


}
