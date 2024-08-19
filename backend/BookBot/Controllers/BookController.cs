using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookBot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private IUnitOfWork _unit{get; set;}

        public BookController(IUnitOfWork Unit)
        {
            _unit = Unit;
        }
        [HttpGet("GetBook")]
        public IActionResult getBook()
        {
            return View();
        }
    }
}