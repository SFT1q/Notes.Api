using Microsoft.EntityFrameworkCore;
using Notes.DataBase.EntityTypeConfigurations;
using Notes.Domain;
using Notes.Domain.DomainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DataBase
{
    public class ApplicationContext : DbContext, INotesDbContext
    {
        public DbSet<Note> Notes { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
            base.OnModelCreating(modelBuilder);           
        }
    }
}
