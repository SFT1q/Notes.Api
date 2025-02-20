using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Interfaces;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataBase.DatabaseRepository
{
    public class NoteRepository : INoteRepository<Note, Guid>
    {
        private readonly ApplicationContext _context;
        public NoteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Guid> AddNote(Note entity)
        {
            _context.Notes.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> DeleteNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) throw new Exception("Note not found");

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return note.Id;
        }

        public async Task<Note> GetNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) throw new Exception("Note not Found");

            return note;
        }

        public async Task<ICollection<Note>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        public async Task<Guid> UpdateNote(Guid id, Note entity)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) throw new Exception("user not found");

            _context.Entry(note).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
