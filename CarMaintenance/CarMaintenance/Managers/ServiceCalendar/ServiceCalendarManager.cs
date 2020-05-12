using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;


namespace CarMaintenance.Managers.ServiceCalendar
{
    public class ServiceCalendarManager
    {
        public List<ServiceCalendarModel> GetServiceCalendarModels(List<CarDetails> carsDetails)
        {
            List<ServiceCalendarModel> serviceCalendarModels = new List<ServiceCalendarModel>();
            foreach (CarDetails carDetails in carsDetails) {
                int? nextRevisionKm = null;
                DateTime? nextRevisionDate = null;
                DateTime? nextPti = null;
                DateTime? nextVig = null;

                if (carDetails.Periodicity.RevisionKm != null) {
                    nextRevisionKm = carDetails.ActualKilometers + (int)carDetails.Periodicity.RevisionKm;
                }
                if (carDetails.Periodicity.RevisionMonths != null) {
                    nextRevisionDate = carDetails.LastRevision.AddMonths((int) carDetails.Periodicity.RevisionMonths);
                }
                if (carDetails.Periodicity.PtiMonths != null)
                {
                    nextPti = carDetails.LastPti.AddMonths((int)carDetails.Periodicity.PtiMonths);
                }
                if (carDetails.Periodicity.VigMonths != null)
                {
                    nextVig = carDetails.LastVig.AddMonths((int)carDetails.Periodicity.VigMonths);
                }

                serviceCalendarModels.Add(new ServiceCalendarModel(nextRevisionKm, nextRevisionDate, nextPti, nextVig));
            }

            return serviceCalendarModels;
        }
    }
}
