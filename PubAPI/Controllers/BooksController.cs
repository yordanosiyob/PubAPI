using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PubAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly PubContext _db;

    public BooksController(PubContext db)
    {
        _db = db;
    }

    // GET - Hämtar alla böcker
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _db.Books
            .Include(b => b.Author)
            .ToListAsync();
        return Ok(books);
    }

    // GET - Hämtar en bok via ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _db.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.BookId == id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }

    // POST - Skapar en ny bok
    [HttpPost]
    public async Task<IActionResult> Create(Book book)
    {
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        return Ok(book);
    }

    // PUT - Uppdaterar en bok
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Book updatedBook)
    {
        var book = await _db.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        book.Title = updatedBook.Title;
        book.PublishDate = updatedBook.PublishDate;
        book.BasePrice = updatedBook.BasePrice;
        book.AuthorId = updatedBook.AuthorId;

        await _db.SaveChangesAsync();
        return Ok(book);
    }

    // DELETE - Tar bort en bok
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.Books.FindAsync(id);

        if (book == null)
            return NotFound();

        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return Ok($"Bok med ID {id} är borttagen.");
    }
}