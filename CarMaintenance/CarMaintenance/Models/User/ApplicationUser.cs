
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;


namespace CarMaintenance.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName="nvarchar(100)")]
        public string FullName { get; set; }
    }
}
