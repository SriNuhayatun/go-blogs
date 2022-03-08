using go_blogs.Models;
using go_blogs.Services.BlogServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_blogs.Controllers
{
    public class LayerController : Controller
    {
        private readonly IBlogServices _blogService;

        public LayerController(IBlogServices s)
        {
            _blogService = s;
        }
        public IActionResult Index()
        {
            var data = _blogService.TampilSemuaData();
            return View(data);
        }

   
    }
}
