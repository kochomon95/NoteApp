using Note_App.Models;

namespace Note_App.IRepositories
{
    public interface INoteRepository
    {
        Task<IEnumerable<Note>> GetAllNotesAsync();
        Task<Note> GetNoteByIdAsync(int id);
        Task<int> CreateNoteAsync(Note note);
        Task<int> UpdateNoteAsync(Note note);
        Task<int> DeleteNoteAsync(int id);
    }
}
