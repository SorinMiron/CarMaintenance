using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMaintenance.Controllers
{
    public interface IAdminPanelControllerInterface
    {

        Task<object> GetCustomers();

        Task<object> RemoveCustomer(object customerId);

    }
}
