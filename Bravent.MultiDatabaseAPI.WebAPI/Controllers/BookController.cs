using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bravent.MultiDatabaseAPI.Domain.Services;
using Bravent.MultiDatabaseAPI.ServiceContracts.Models;
using Bravent.MultiDatabaseAPI.WebAPI.Helpers;

namespace Bravent.MultiDatabaseAPI.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet("byisbn")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO>> FindByISBN([FromQuery] string isbn)
        {
            try
            {
                return new ApiResult<BookDTO>(await _service.FindByISBN(isbn));
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }

        [HttpGet("byname")]
        [ProducesResponseType(typeof(IEnumerable<BookDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> FindByName([FromQuery] string name)
        {
            try
            {
                return new ApiResult<IEnumerable<BookDTO>>(await _service.FindByName(name));
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }

        [HttpGet("byid")]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO>> FindById([FromQuery]string id)
        {
            try
            {
                return new ApiResult<BookDTO>(await _service.FindById(id));
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/json")]
        public async Task<ActionResult<BookDTO>> Create([FromBody] BookDTO book)
        {
            try
            {
                return new ApiResult<BookDTO>(await _service.Insert(book), StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return new ExceptionResult(ex);
            }
        }
    }
}
