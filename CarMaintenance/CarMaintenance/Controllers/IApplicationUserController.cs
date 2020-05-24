using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Mvc;


namespace CarMaintenance.Controllers
{
    public interface IApplicationUserController
    {
        Task<object> PostApplicationUser(ApplicationUserModel applicationUserModel);
        
        Task<IActionResult> Login(LoginModel loginModel);


    }
}
