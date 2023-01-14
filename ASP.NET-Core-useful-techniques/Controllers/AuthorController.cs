namespace ASP.NET_Core_useful_techniques.Controllers
{
    using ASP.NET.Core.Useful.Techniques.Services.Interfaces;
    using ASP.NET.Core.Useful.Techniques.Services.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await service.GetAllAuthorsAsync();

            return this.Ok(authors);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] IEnumerable<AuthorInputModel> model)
        {
            if (!ModelState.IsValid)
            {
                throw new ArgumentException("Models input parameters are invalid");
            }

            await this.service.CreateAuthorAsync(model);

            return this.Ok();
        }
    }
}
