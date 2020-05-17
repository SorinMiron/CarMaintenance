using System.Collections.Generic;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;


namespace CarMaintenance.Managers.ServiceCalendar
{
    public interface IServiceCalendarManager
    {
        List<ServiceCalendarModel> GetServiceCalendarModels(List<CarDetails> carsDetails);
    }
}
