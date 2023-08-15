using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksContext _context;

    public BooksController(BooksContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _context.Books.Find(book => true).ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(string id)
    {
        var book = await _context.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        await _context.Books.InsertOneAsync(book);
        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(string id, Book updatedBook)
    {
        var existingBook = await _context.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
        if (existingBook == null)
        {
            return NotFound();
        }
        updatedBook.Id = id;
        await _context.Books.ReplaceOneAsync(book => book.Id == id, updatedBook);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(string id)
    {
        var bookToDelete = await _context.Books.Find(book => book.Id == id).FirstOrDefaultAsync();
        if (bookToDelete == null)
        {
            return NotFound();
        }
        await _context.Books.DeleteOneAsync(book => book.Id == id);
        return NoContent();
    }
}
