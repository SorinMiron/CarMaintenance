using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CarMaintenance.Models.Customer
{
    public class CustomerModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string CarList { get; set; }
        public CustomerModel()
        {

        }

        public CustomerModel(string id, string userName, string fullName = null, string email = null, string carList = null)
        {
            //todo add validations : id and username required
            Id = id;
            UserName = userName;
            FullName = fullName;
            Email = email;
            CarList = carList;
        }
    }
}
