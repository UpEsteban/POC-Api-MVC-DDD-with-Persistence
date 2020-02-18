using Bravent.MultiDatabaseAPI.Domain.Services;
using Bravent.MultiDatabaseAPI.ServiceContracts.Models;
using Bravent.MultiDatabaseAPI.WebAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bravent.MultiDatabaseAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _service;

        public AuthorController(IAuthorService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<AuthorDTO>> Create([FromBody] AuthorDTO author)
        {
            try
            {
                return new ApiResult<AuthorDTO>(await _service.Insert(author), StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }
    }
}
