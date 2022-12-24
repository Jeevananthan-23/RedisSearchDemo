using Microsoft.AspNetCore.Mvc;
using RedisSearchDemo.Models;
using RedisSearchDemo.Repositories;
using System.Net;

namespace RedisSearchDemo.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BookController : ControllerBase
  {
    private readonly BookRepository _bookRepository;

    public BookController(BookRepository bookRepository)
    {
    _bookRepository = bookRepository; 
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Book>> AddBook([FromBody] Book book)
    {
    await _bookRepository.AddBook(book);
    return CreatedAtRoute("GetBookByTitle", new { title = book.Title }, book);
    }

    [Route("[action]/{title}", Name = "GetBookByTitle")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Book>>> GetBookByTitle(string title)
    {
    var books = await _bookRepository.GetBookByTitle(title);
    return Ok(books);
    }

    [Route("[action]", Name = "GetAllBook")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
    var books = await _bookRepository.GetAllBooks();
    return Ok(books);
    }

    [Route("[action]/{query}", Name = "TitleAutoComplete")]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Suggestion>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<List<string>>> TitleAutoComplete(string query)
    {
    var authorsuggestion = await _bookRepository.GetTitleAutoComplete(query);
    return Ok(authorsuggestion);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBooks(
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 10,
            [FromQuery] string q = "*",
            [FromQuery] string sortBy = "title",
            [FromQuery] string direction = "ASC")
    {
    try
    {
      var books = await _bookRepository.PaginateBooks(q, page, sortBy, direction, pageSize);
      return Ok(books);
    }
    catch (Exception)
    {

      return NoContent();
    }
    }

  }
}
