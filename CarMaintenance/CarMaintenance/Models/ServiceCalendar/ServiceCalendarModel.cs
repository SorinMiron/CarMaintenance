using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.ServiceCalendar
{
    public class ServiceCalendarModel
    {
        public string CarNameAndYear { get; set; }
        public int ActualKilometers { get; set; }
        public int? NextRevisionKm { get; set; }
        public DateTime? NextRevisionDate { get; set; }
        public DateTime? NextPti { get; set; }
        public DateTime? NextVig { get; set; }
        public DateTime? NextInsurance { get; set; }
        public RemainingCalendar RemainingCalendar { get; set; }

        public ServiceCalendarModel()
        {

        }

        public ServiceCalendarModel(string carNameAndYear, int actualKilometers, int? nextRevisionKm, DateTime? nextRevisionDate, DateTime? nextPti, DateTime? nextVig, DateTime? nextInsurance)
        {
            CarNameAndYear = carNameAndYear;
            ActualKilometers = actualKilometers;
            NextRevisionKm = nextRevisionKm;
            NextRevisionDate = nextRevisionDate;
            NextPti = nextPti;
            NextVig = nextVig;
            NextInsurance = nextInsurance;
            RemainingCalendar = new RemainingCalendar(this);
        }
    }

}
