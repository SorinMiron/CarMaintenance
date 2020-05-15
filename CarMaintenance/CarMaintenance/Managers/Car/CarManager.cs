using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CarMaintenance.Managers.Car
{
    public class CarManager
    {
        private readonly CarContext _carContext;
        public CarManager(CarContext carContext)
        {
            _carContext = carContext;
        }

        public async Task<object> InsertCar(CarDetails carDetails)
        {
            try
            {
                await _carContext.Cars.AddAsync(carDetails);
                return await _carContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<object> RemoveCar(int id)
        {
            try
            {

                CarDetails carDetails = await _carContext.Cars.FindAsync(id);
                _carContext.Cars.Remove(carDetails);
                return await _carContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CarDetails> GetCarsByUserId(string userId)
        {
            try
            {
                return _carContext.Cars.Include(m => m.Periodicity).Where(car => car.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void RemoveCarsByUserId(string userId)
        {
            try
            {
                List<CarDetails> carForRemoving = _carContext.Cars.Where(car => car.UserId == userId).ToList();
                _carContext.RemoveRange(carForRemoving);
                _carContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CarPeriodicityModel> GetCarsPeriodicityByUserId(string userId)
        {
            try
            {
                List<CarDetails> carDetails = _carContext.Cars.Include(c => c.Periodicity).Where(car => car.UserId == userId).ToList();
                return (from carDetail in carDetails
                        let carDetailAndYear = $"{carDetail.Name} {carDetail.Year}"
                        select new CarPeriodicityModel(carDetail.Id, carDetailAndYear, carDetail.Periodicity.RevisionKm, carDetail.Periodicity.RevisionMonths, carDetail.Periodicity.PtiMonths, carDetail.Periodicity.VigMonths, carDetail.Periodicity.InsuranceMonths)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Object> UpdateCarPeriodicity(string userId, CarPeriodicityModel periodicityModel)
        {
            try
            {
                _carContext.Cars.Include(c => c.Periodicity)
                     .First(m => m.UserId == userId && m.Id == periodicityModel.CarId).Periodicity = new CarPeriodicity(periodicityModel);
                return await _carContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Object> UpdateCar(string userId, CarUpdateModel carDetails)
        {
            try
            {
                CarDetails carForUpdate = _carContext.Cars.First(m => m.UserId == userId && m.Id == carDetails.CarId);

                UpdatePropertiesCar(ref carForUpdate, carDetails);

                return await _carContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static void UpdatePropertiesCar(ref CarDetails carForUpdate, CarUpdateModel carDetails)
        {
            carForUpdate.ActualKilometers = carDetails.ActualKilometers;
            carForUpdate.LastRevisionDate = carDetails.LastRevisionDate;
            carForUpdate.LastRevisionKm = carDetails.LastRevisionKm;
            carForUpdate.LastPti = carDetails.LastPti;
            carForUpdate.LastVig = carDetails.LastVig;
            carForUpdate.LastInsurance = carDetails.LastInsurance;
        }

    }
}
