
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using CarMaintenance.Models.Car;

using Microsoft.AspNetCore.Identity;


namespace CarMaintenance.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName="nvarchar(100)")]
        public string FullName { get; set; }

    }
}
