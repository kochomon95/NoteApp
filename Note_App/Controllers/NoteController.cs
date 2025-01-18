using Microsoft.AspNetCore.Mvc;
using Note_App.IRepositories;
using Note_App.Models;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteRepository _noteRepository;

    public NotesController(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }

    // GET: api/Notes
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await _noteRepository.GetAllNotesAsync();
        return Ok(notes);
    }

    // GET: api/Notes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNoteById(int id)
    {
        var note = await _noteRepository.GetNoteByIdAsync(id);
        if (note == null)
            return NotFound();
        return Ok(note);
    }

    // POST: api/Notes
    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] Note note)
    {
        if (note == null)
            return BadRequest();
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;
        await _noteRepository.CreateNoteAsync(note);
        return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
    }

    // PUT: api/Notes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, [FromBody] Note note)
    {
        if (note == null || note.Id != id)
            return BadRequest();
        note.UpdatedAt = DateTime.UtcNow;
        await _noteRepository.UpdateNoteAsync(note);
        return NoContent();
    }

    // DELETE: api/Notes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var result = await _noteRepository.DeleteNoteAsync(id);
        if (result == 0)
            return NotFound();
        return NoContent();
    }
}
