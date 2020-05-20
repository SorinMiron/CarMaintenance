using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;


namespace CarMaintenance.Managers.ServiceCalendar
{
    public class ServiceCalendarManager : IServiceCalendarManager
    {
        private ServiceCalendarManager()
        {

        }
        public List<ServiceCalendarModel> GetServiceCalendarModels(List<CarDetails> carsDetails)
        {
            try
            {

                List<ServiceCalendarModel> serviceCalendarModels = new List<ServiceCalendarModel>();
                foreach (CarDetails carDetails in carsDetails)
                {
                    if (carDetails.Periodicity == null) {
                        throw new NullReferenceException(nameof(carDetails.Periodicity));
                    }
                    int? nextRevisionKm = null;
                    DateTime? nextRevisionDate = null;
                    DateTime? nextPti = null;
                    DateTime? nextVig = null;
                    DateTime? nextInsurance = null;

                    if (carDetails.Periodicity.RevisionKm != null)
                    {
                        nextRevisionKm = carDetails.LastRevisionKm + (int)carDetails.Periodicity.RevisionKm;
                    }

                    if (carDetails.Periodicity.RevisionMonths != null)
                    {
                        nextRevisionDate = carDetails.LastRevisionDate.AddMonths((int)carDetails.Periodicity.RevisionMonths);
                    }

                    if (carDetails.Periodicity.PtiMonths != null)
                    {
                        nextPti = carDetails.LastPti.AddMonths((int)carDetails.Periodicity.PtiMonths);
                    }

                    if (carDetails.Periodicity.VigMonths != null)
                    {
                        nextVig = carDetails.LastVig.AddMonths((int)carDetails.Periodicity.VigMonths);
                    }

                    if (carDetails.Periodicity.InsuranceMonths != null)
                    {
                        nextInsurance = carDetails.LastInsurance.AddMonths((int)carDetails.Periodicity.InsuranceMonths);
                    }

                    serviceCalendarModels.Add(new ServiceCalendarModel($"{carDetails.Name} {carDetails.Year}", carDetails.ActualKilometers,
                        nextRevisionKm, nextRevisionDate, nextPti, nextVig, nextInsurance));
                }

                return serviceCalendarModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
