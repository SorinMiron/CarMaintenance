using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;
using CarMaintenance.Models.ServiceCalendar;


namespace CarMaintenance.Models.ServiceCalendar
{
    public class RemainingCalendar
    {

        public int? RemainingRevisionKm { get; set; }
        public int? RemainingRevisionDays { get; set; }
        public int? RemainingPtiDays { get; set; }
        public int? RemainingVigDays { get; set; }
        public int? RemainingInsuranceDays { get; set; }

        public RemainingCalendar(int? remainingRevisionKm, int? remainingRevisionDays, int? remainingPtiDays, int? remainingVigDays, int? remainingInsuranceDays)
        {
            RemainingRevisionKm = remainingRevisionKm;
            RemainingRevisionDays = remainingRevisionDays;
            RemainingPtiDays = remainingPtiDays;
            RemainingVigDays = remainingVigDays;
            RemainingInsuranceDays = remainingInsuranceDays;
        }

        public RemainingCalendar(ServiceCalendarModel serviceCalendarModel)
        {
            RemainingRevisionKm = serviceCalendarModel.NextRevisionKm != null
                ? serviceCalendarModel.NextRevisionKm - serviceCalendarModel.ActualKilometers
                : null;
            RemainingRevisionDays = getDaysFromDateTime(serviceCalendarModel.NextRevisionDate);
            RemainingPtiDays = getDaysFromDateTime(serviceCalendarModel.NextPti);
            RemainingVigDays = getDaysFromDateTime(serviceCalendarModel.NextVig);
            RemainingInsuranceDays = getDaysFromDateTime(serviceCalendarModel.NextInsurance);
        }

        private int? getDaysFromDateTime(DateTime? dateTime)
        {
            double? daysAsDouble = (dateTime - DateTime.Now)?.TotalDays + 1;

            return daysAsDouble.HasValue ? (int?)Math.Round(daysAsDouble.Value) : null;
        }
    }
}
