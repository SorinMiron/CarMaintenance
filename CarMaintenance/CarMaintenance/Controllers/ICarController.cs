using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Car;


namespace CarMaintenance.Controllers
{
    public interface ICarController
    {

        Task<object> InsertCar(CarDetailsModel carDetails);

        List<CarDetails> GetCarsByUserId();

        Task<object> RemoveCar(object id);

        Task<object> UpdateCar(CarUpdateModel carUpdateModel);

    }
}
