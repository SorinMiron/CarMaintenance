using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;


namespace CarMaintenance.Controllers
{
    public interface IServiceCalendarController
    {
        List<ServiceCalendarModel> GetServiceCalendar();
    }
}
