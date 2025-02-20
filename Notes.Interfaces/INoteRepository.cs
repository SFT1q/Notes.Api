using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Interfaces
{
    public interface INoteRepository<IEntity, TId> where IEntity : class
    {
        public Task<ICollection<IEntity>> GetNotes();
        public Task<IEntity> GetNote(TId id);
        public Task<TId> AddNote(IEntity entity);
        public Task<TId> DeleteNote(TId id);
        public Task<TId> UpdateNote(TId id, IEntity entity);
    }
}
