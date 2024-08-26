using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IUnitOfWork _unit { get; set; }
        public AuthorsController(IUnitOfWork unit)
        {
            _unit = unit;
        }
        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<List<Author>>> returnAllAuthors()
        {
            return Ok(await _unit.author.ReturnAllAuthors());
        }
        [HttpGet("GetAuthor")]
        public async Task<ActionResult<List<Author>>> getAuthor(string s)
        {
            return Ok(await _unit.author.AuthorSearch(s));
        }
    }
}
