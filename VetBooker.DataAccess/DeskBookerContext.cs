using VetBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace VetBooker.DataAccess
{
  public class VetBookerContext : DbContext
  {
    public VetBookerContext(DbContextOptions<VetBookerContext> options) : base(options)
    {
    }

    public DbSet<VetBooking> VetBooking { get; set; }

    public DbSet<Vet> Vet { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Vet>().HasData(
        new Vet { Id = 1, Description = "Vet 1" },
        new Vet { Id = 2, Description = "Vet 2" }
      );
    }
  }
}