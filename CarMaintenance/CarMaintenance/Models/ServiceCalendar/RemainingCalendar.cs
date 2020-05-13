using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;


namespace CarMaintenance.Models.ServiceCalendar
{
    public class RemainingCalendar
    {

        public int? RemainingRevisionKm { get; set; }
        public TimeSpan? RemainingRevisionDate { get; set; }
        public TimeSpan? RemainingPti { get; set; }
        public TimeSpan? RemainingVig { get; set; }
        public TimeSpan? RemainingInsurance { get; set; }

        public RemainingCalendar(int? remainingRevisionKm, TimeSpan? remainingRevisionDate, TimeSpan? remainingPti, TimeSpan? remainingVig, TimeSpan? remainingInsurance)
        {
            RemainingRevisionKm = remainingRevisionKm;
            RemainingRevisionDate = remainingRevisionDate;
            RemainingPti = remainingPti;
            RemainingVig = remainingVig;
            RemainingInsurance = remainingInsurance;
        }

        public RemainingCalendar(ServiceCalendarModel serviceCalendarModel)
        {
            RemainingRevisionKm = serviceCalendarModel.NextRevisionKm != null
                ? serviceCalendarModel.NextRevisionKm - serviceCalendarModel.ActualKilometers
                : null;

            RemainingRevisionDate = serviceCalendarModel.NextRevisionDate != null ? DateTime.Now - serviceCalendarModel.NextRevisionDate : null;
            RemainingPti = serviceCalendarModel.NextPti != null ? DateTime.Now - serviceCalendarModel.NextPti : null;
            RemainingVig = serviceCalendarModel.NextVig != null ? DateTime.Now - serviceCalendarModel.NextVig : null;
            RemainingInsurance = serviceCalendarModel.NextInsurance != null ? DateTime.Now - serviceCalendarModel.NextInsurance : null;
        }
    }
}
