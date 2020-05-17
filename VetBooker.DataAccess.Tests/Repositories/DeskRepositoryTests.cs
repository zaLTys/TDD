using VetBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VetBooker.DataAccess.Repositories
{
  public class VetRepositoryTests
  {
    [Fact]
    public void ShouldReturnTheAvailableVets()
    {
      // Arrange
      var date = new DateTime(2020, 1, 25);

      var options = new DbContextOptionsBuilder<VetBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldReturnTheAvailableVets")
        .Options;

      using (var context = new VetBookerContext(options))
      {
        context.Vet.Add(new Vet { Id = 1 });
        context.Vet.Add(new Vet { Id = 2 });
        context.Vet.Add(new Vet { Id = 3 });

        context.VetBooking.Add(new VetBooking { VetId = 1, Date = date });
        context.VetBooking.Add(new VetBooking { VetId = 2, Date = date.AddDays(1) });

        context.SaveChanges();
      }

      using (var context = new VetBookerContext(options))
      {
        var repository = new VetRepository(context);

        // Act
        var vets = repository.GetAvailableVets(date);

        // Assert
        Assert.Equal(2, vets.Count());
        Assert.Contains(vets, d => d.Id == 2);
        Assert.Contains(vets, d => d.Id == 3);
        Assert.DoesNotContain(vets, d => d.Id == 1);
      }
    }

    [Fact]
    public void ShouldGetAll()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<VetBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldGetAll")
        .Options;

      var storedList = new List<Vet>
      {
        new Vet(),
        new Vet(),
        new Vet()
      };

      using (var context = new VetBookerContext(options))
      {
        foreach (var vet in storedList)
        {
          context.Add(vet);
          context.SaveChanges();
        }
      }

      // Act
      List<Vet> actualList;
      using (var context = new VetBookerContext(options))
      {
        var repository = new VetRepository(context);
        actualList = repository.GetAll().ToList();
      }

      // Assert
      Assert.Equal(storedList.Count(), actualList.Count());
    }
  }
}