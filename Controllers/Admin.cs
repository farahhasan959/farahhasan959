using RightProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farah.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "Cookies")]
    public class Admin : Controller
    {
        private readonly finalContext db;
        public static string Message { get; set; }
        public static string successMsg { get; set; }

        public Admin(finalContext context)
        {
            db = context;
        }
        public IActionResult Home()
        {
            return View();
        }
    }
}
