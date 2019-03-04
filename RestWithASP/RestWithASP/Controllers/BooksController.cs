using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithASP.Business;
using RestWithASP.Data.VO;
using RestWithASP.Model;

namespace RestWithASP.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : Controller
    {
        //Declaração do serviço usado
        private IBookBusiness _bookBusiness;

        /* Injeção de uma instancia de IBookBusiness ao criar
        uma instancia de BookController */
        public BooksController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        //Mapeia as requisições GET para http://localhost:{porta}/api/books/v1/
        //Get sem parâmetros para o FindAll --> Busca Todos
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());
        }


        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var book = _bookBusiness.FindById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
            //return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]BookVO book)
        {
            if (book == null)
                return BadRequest();

            return new ObjectResult(_bookBusiness.Create(book));
            //return Ok();
        }


        [HttpPut]
        public IActionResult Put([FromBody]BookVO book)
        {
            if (book == null)
                return BadRequest();

            var updateBook = _bookBusiness.Update(book);
            if (updateBook == null)
                return BadRequest();

            return new ObjectResult(updateBook);
            //return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusiness.Delete(id);
            return NoContent();

            //return Ok();
        }

    }
}