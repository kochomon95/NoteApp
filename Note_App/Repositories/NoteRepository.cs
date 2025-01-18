using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Note_App.Models;
using Note_App.IRepositories;
using Note_App.DatabaseContext;

namespace Note_App.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDBContext _dbContext;

        // Inject ApplicationDBContext to access the connection string
        public NoteRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create connection 
        private IDbConnection DbConnection() => _dbContext.CreateConnection();

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            using var connection = DbConnection();
            var query = "SELECT * FROM Notes";
            return await connection.QueryAsync<Note>(query);
        }

        public async Task<Note> GetNoteByIdAsync(int id)
        {
            using var connection = DbConnection();
            var query = "SELECT * FROM Notes WHERE Id = @Id";
            return await connection.QuerySingleOrDefaultAsync<Note>(query, new { Id = id });
        }

        public async Task<int> CreateNoteAsync(Note note)
        {
            using var connection = DbConnection();
            var query = "INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt) VALUES (@Title, @Content, @CreatedAt, @UpdatedAt)";
            return await connection.ExecuteAsync(query, new
            {
                note.Title,
                note.Content,
                note.CreatedAt,
                note.UpdatedAt
            });
        }

        public async Task<int> UpdateNoteAsync(Note note)
        {
            using var connection = DbConnection();
            var query = "UPDATE Notes SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            return await connection.ExecuteAsync(query, new
            {
                note.Title,
                note.Content,
                note.UpdatedAt,
                note.Id
            });
        }

        public async Task<int> DeleteNoteAsync(int id)
        {
            using var connection = DbConnection();
            var query = "DELETE FROM Notes WHERE Id = @Id";
            return await connection.ExecuteAsync(query, new { Id = id });
        }
    }
}
