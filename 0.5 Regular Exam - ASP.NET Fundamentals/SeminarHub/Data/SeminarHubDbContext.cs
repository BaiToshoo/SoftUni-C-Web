using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data.Models;

namespace SeminarHub.Data
{
    public class SeminarHubDbContext : IdentityDbContext
    {
        //Constructor
        public SeminarHubDbContext(DbContextOptions<SeminarHubDbContext> options)
            : base(options)
        {
        }
        //DbSets (tables)
        public DbSet<Seminar> Seminars { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<SeminarParticipant> SeminarsParticipants { get; set; } = null!;

        //Models Creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Creating The Composite Key
            modelBuilder.Entity<SeminarParticipant>()
                .HasKey(sp => new { sp.ParticipantId, sp.SeminarId });

            //Dealing with circular reference
            modelBuilder.Entity<SeminarParticipant>()
                .HasOne(sp => sp.Seminar)
                .WithMany(s => s.SeminarsParticipants)
                .OnDelete(DeleteBehavior.Restrict);

            //Seeding the database with categories
            modelBuilder
               .Entity<Category>()
               .HasData(new Category()
               {
                   Id = 1,
                   Name = "Technology & Innovation"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Business & Entrepreneurship"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Science & Research"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Arts & Culture"
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}