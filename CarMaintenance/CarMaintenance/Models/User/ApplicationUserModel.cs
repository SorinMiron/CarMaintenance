using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarMaintenance.Models.Car;


namespace CarMaintenance.Models.User
{
    public class ApplicationUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
