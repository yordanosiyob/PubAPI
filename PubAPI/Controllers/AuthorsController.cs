using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

namespace PubAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly PubContext _db;

    public AuthorsController(PubContext db)
    {
        _db = db;
    }

    // GET - Hämtar alla författare
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var authors = await _db.Authors
            .Include(a => a.Books)
            .ToListAsync();
        return Ok(authors);
    }

    // GET - Hämtar en författare via ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _db.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.AuthorId == id);

        if (author == null)
            return NotFound();

        return Ok(author);
    }

    // POST - Skapar en ny författare
    [HttpPost]
    public async Task<IActionResult> Create(Author author)
    {
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
        return Ok(author);
    }

    // PUT - Uppdaterar en författare
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Author updatedAuthor)
    {
        var author = await _db.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        author.FirstName = updatedAuthor.FirstName;
        author.LastName = updatedAuthor.LastName;

        await _db.SaveChangesAsync();
        return Ok(author);
    }

    // DELETE - Tar bort en författare
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _db.Authors.FindAsync(id);

        if (author == null)
            return NotFound();

        _db.Authors.Remove(author);
        await _db.SaveChangesAsync();
        return Ok($"Författare med ID {id} är borttagen.");
    }
}