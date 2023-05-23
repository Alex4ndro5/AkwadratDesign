using AkwadratDesign.Models.DbModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AkwadratDesign.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectFirm> ProjectFirms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
            .HasMany(e => e.Firms)
            .WithMany(e => e.Projects)
            .UsingEntity<ProjectFirm>(
             l => l.HasOne<Firm>(e => e.Firm).WithMany(e => e.ProjectFirms).HasForeignKey(e => e.FirmsId),
             r => r.HasOne<Project>(e => e.Project).WithMany(e => e.ProjectFirms).HasForeignKey(e => e.ProjectsId));

            modelBuilder.Entity<Client>()
            .HasMany(c => c.Projects)
            .WithOne(p => p.Client)
            .HasForeignKey(p => p.ClientId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Databse=master;Trusted_Connection=True;", builder => builder.EnableRetryOnFailure());
            }
        }
    }
}