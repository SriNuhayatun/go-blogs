using go_blogs.Services.BlogServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        //private readonly IBlogServices _service;
        //public HomeController(IBlogServices s)
        //{
        //    _service = s;
        //}
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Masuk()
        {
            return View();
        }

    }
}
