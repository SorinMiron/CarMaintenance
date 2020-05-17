using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;
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
            catch (Exception ex) {
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

        public void ValidateCarDetailsModel(CarDetailsModel carDetails)
        {
            if (carDetails == null)
            {
                throw new ArgumentNullException(nameof(carDetails));
            }
            if (string.IsNullOrWhiteSpace(carDetails.Name))
            {
                throw new ArgumentNullException(nameof(carDetails.Name));
            }
            if (carDetails.Year <= 0 || carDetails.Year > DateTime.Today.Year)
            {
                throw new ArgumentOutOfRangeException(nameof(carDetails.Year));
            }
            if (carDetails.LastRevisionKm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carDetails.LastRevisionKm));
            }
            if (carDetails.ActualKilometers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carDetails.ActualKilometers));
            }
            if (carDetails.LastRevisionKm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carDetails.LastRevisionKm));
            }
            if (carDetails.LastRevisionDate == null)
            {
                throw new ArgumentNullException(nameof(carDetails.LastRevisionDate));
            }
            if (carDetails.LastVig == null)
            {
                throw new ArgumentNullException(nameof(carDetails.LastVig));
            }
            if (carDetails.LastPti == null)
            {
                throw new ArgumentNullException(nameof(carDetails.LastPti));
            }
            if (carDetails.LastInsurance == null)
            {
                throw new ArgumentNullException(nameof(carDetails.LastInsurance));
            }
        }

        public void ValidateCarUpdateModel(CarUpdateModel carUpdateModel)
        {
            if (carUpdateModel == null)
            {
                throw new ArgumentNullException(nameof(carUpdateModel));
            }
            if (carUpdateModel.CarId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carUpdateModel.CarId));
            }
            if (carUpdateModel.LastRevisionKm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carUpdateModel.LastRevisionKm));
            }
            if (carUpdateModel.ActualKilometers < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carUpdateModel.ActualKilometers));
            }
            if (carUpdateModel.LastRevisionKm < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carUpdateModel.LastRevisionKm));
            }
            if (carUpdateModel.LastRevisionDate == null)
            {
                throw new ArgumentNullException(nameof(carUpdateModel.LastRevisionDate));
            }
            if (carUpdateModel.LastVig == null)
            {
                throw new ArgumentNullException(nameof(carUpdateModel.LastVig));
            }
            if (carUpdateModel.LastPti == null)
            {
                throw new ArgumentNullException(nameof(carUpdateModel.LastPti));
            }
            if (carUpdateModel.LastInsurance == null)
            {
                throw new ArgumentNullException(nameof(carUpdateModel.LastInsurance));
            }
        }
        public void ValidateCarPeriodicityModel(CarPeriodicityModel carPeriodicityModel)
        {
            if (carPeriodicityModel == null)
            {
                throw new ArgumentNullException(nameof(carPeriodicityModel));
            }
            if (carPeriodicityModel.CarId < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.CarId));
            }
            if (carPeriodicityModel.RevisionKm.HasValue && (carPeriodicityModel.RevisionKm < 1000 || carPeriodicityModel.RevisionKm > 50000))
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.RevisionKm));
            }
            if (carPeriodicityModel.RevisionMonths.HasValue && (carPeriodicityModel.RevisionMonths < 1 || carPeriodicityModel.RevisionMonths > 36))
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.RevisionMonths));
            }
            if (carPeriodicityModel.PtiMonths.HasValue && (carPeriodicityModel.PtiMonths < 1 || carPeriodicityModel.PtiMonths > 36))
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.PtiMonths));
            }
            if (carPeriodicityModel.VigMonths.HasValue && (carPeriodicityModel.VigMonths < 1 || carPeriodicityModel.VigMonths > 36))
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.VigMonths));
            }
            if (carPeriodicityModel.InsuranceMonths.HasValue && (carPeriodicityModel.InsuranceMonths < 1 || carPeriodicityModel.InsuranceMonths > 36))
            {
                throw new ArgumentOutOfRangeException(nameof(carPeriodicityModel.InsuranceMonths));
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
