
using Blazor_Forum.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazor_Forum.Data
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<Reponse> Reponses { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasMany(m => m.Reponses)
                .WithOne(r => r.Message)
                .HasForeignKey(r => r.MessageId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Reponse>()
                .HasMany(r => r.Likes)
                .WithOne(l => l.Reponse)
                .HasForeignKey(l => l.ReponseId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Like>()
                .HasIndex(l => new { l.UserId, l.ReponseId })
                .IsUnique();
            // Index pour les performances
            modelBuilder.Entity<Reponse>()
                        .HasIndex(r => r.MessageId);

            modelBuilder.Entity<Like>()
                .HasIndex(l => l.ReponseId);
        }
    }
}
