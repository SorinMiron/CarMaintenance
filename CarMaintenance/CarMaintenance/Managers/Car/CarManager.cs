using System;
using System.Threading.Tasks;
using CarMaintenance.Models.Car;

using Microsoft.EntityFrameworkCore.ChangeTracking;


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
    }
}
