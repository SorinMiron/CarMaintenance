using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Models.ServiceCalendar
{
    public class ServiceCalendarModel
    {
        public int? NextRevisionKm { get; set; }
        public DateTime? NextRevisionDate { get; set; }
        public DateTime? NextPti { get; set; }
        public DateTime? NextVig { get; set; }


        public ServiceCalendarModel()
        {

        }

        public ServiceCalendarModel(int? nextRevisionKm, DateTime? nextRevisionDate, DateTime? nextPti, DateTime? nextVig)
        {
            NextRevisionKm = nextRevisionKm;
            NextRevisionDate = nextRevisionDate;
            NextPti = nextPti;
            NextVig = nextVig;
        }
    }

}
