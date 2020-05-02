using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Models.Car;

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
            try {
                await _carContext.Cars.AddAsync(carDetails);
                return await _carContext.SaveChangesAsync();
            }
            catch (Exception ex) {
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
            try {
                return _carContext.Cars.Where(car => car.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
