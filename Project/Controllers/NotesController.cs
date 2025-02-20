using Microsoft.AspNetCore.Mvc;
using Notes.Domain;
using Notes.Interfaces;

    namespace Notes.WebApi.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class NotesController : ControllerBase
        {
            private readonly INoteRepository<Note, Guid> _noteRepository;
            public NotesController(INoteRepository<Note, Guid> noteRepository)
            {
                _noteRepository = noteRepository;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
            {
                var notes = await _noteRepository.GetNotes();
                return Ok(notes);
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Note>> GetNote(Guid id)
            {
                var note = await _noteRepository.GetNote(id);
                if (note == null)
                {
                    return NotFound("Note Not Found");
                }
                return Ok(note);
            }

            [HttpPost]
            public async Task<ActionResult<Guid>> AddNote([FromBody] Note note)
            {
                if (note == null)
                {
                    return NotFound("Note Not Found");
                }

                note.Id = Guid.NewGuid();
                note.Created = DateTime.UtcNow;
                note.Updated = DateTime.UtcNow;

                var noteId = await _noteRepository.AddNote(note);
                return CreatedAtAction(nameof(GetNote), new { id = noteId }, noteId);
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateNote(Guid id, [FromBody] Note note)
            {
                if (note == null)
                {
                    return NotFound("Note Not Found");
                }

                note.Updated = DateTime.UtcNow;
                await _noteRepository.UpdateNote(id, note);
                return NoContent();
            }
            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteNote(Guid id)
            {
                await _noteRepository.DeleteNote(id);
                return NoContent();
            }      
        }
    }
