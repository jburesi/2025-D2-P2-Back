using EvalApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvalApp.Repository
{
    public class EvalAppDbContext : DbContext
    {
        public EvalAppDbContext(DbContextOptions<EvalAppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            this.EventTableInstantiate(modelBuilder);
        }

        private void EventTableInstantiate(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
                        {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Description);
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Location).IsRequired();
            });
        }
    }
}
