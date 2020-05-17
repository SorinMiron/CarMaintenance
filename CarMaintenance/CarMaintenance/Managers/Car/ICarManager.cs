using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace CarMaintenance.Managers.Car
{
    public interface ICarManager
    {
        Task<object> InsertCar(CarDetails carDetails);
        Task<object> RemoveCar(int id);
        List<CarDetails> GetCarsByUserId(string userId);
        void RemoveCarsByUserId(string userId);
        List<CarPeriodicityModel> GetCarsPeriodicityByUserId(string userId);
        Task<Object> UpdateCarPeriodicity(string userId, CarPeriodicityModel periodicityModel);
        Task<Object> UpdateCar(string userId, CarUpdateModel carDetails);
        void ValidateCarDetailsModel(CarDetailsModel carDetails);
        void ValidateCarUpdateModel(CarUpdateModel carUpdateModel);
        void ValidateCarPeriodicityModel(CarPeriodicityModel carPeriodicityModel);
    }
}
