using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Periodicity;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CarMaintenance.Controllers
{
    public interface IPeriodicityController
    {
        List<CarPeriodicityModel> GetCarsPeriodicityByUserId();
        Task<object> UpdatePeriodicity(CarPeriodicityModel carPeriodicityModel);

    }
}
