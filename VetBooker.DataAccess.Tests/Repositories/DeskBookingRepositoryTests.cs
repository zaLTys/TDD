using VetBooker.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace VetBooker.DataAccess.Repositories
{
  public class VetBookingRepositoryTests
  {
    [Fact]
    public void ShouldSaveTheVetBooking()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<VetBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldSaveTheVetBooking")
        .Options;

      var vetBooking = new VetBooking
      {
        FirstName = "Paul",
        LastName = "Simons",
        Date = new DateTime(2020, 1, 25),
        Email = "Paul@PaulSimons.com",
        Pet = "Katinas",
        VetId = 1
      };

      // Act
      using (var context = new VetBookerContext(options))
      {
        var repository = new VetBookingRepository(context);
        repository.Save(vetBooking);
      }

      // Assert
      using (var context = new VetBookerContext(options))
      {
        var bookings = context.VetBooking.ToList();
        var storedVetBooking = Assert.Single(bookings);

        Assert.Equal(vetBooking.FirstName, storedVetBooking.FirstName);
        Assert.Equal(vetBooking.LastName, storedVetBooking.LastName);
        Assert.Equal(vetBooking.Email, storedVetBooking.Email);
        Assert.Equal(vetBooking.Pet, storedVetBooking.Pet);
        Assert.Equal(vetBooking.VetId, storedVetBooking.VetId);
        Assert.Equal(vetBooking.Date, storedVetBooking.Date);
      }
    }

    [Fact]
    public void ShouldGetAllOrderedByDate()
    {
      // Arrange
      var options = new DbContextOptionsBuilder<VetBookerContext>()
        .UseInMemoryDatabase(databaseName: "ShouldGetAllOrderedByDate")
        .Options;

      var storedList = new List<VetBooking>
      {
        CreateVetBooking(1,new DateTime(2020, 1, 27)),
        CreateVetBooking(2,new DateTime(2020, 1, 25)),
        CreateVetBooking(3,new DateTime(2020, 1, 29))
      };

      var expectedList = storedList.OrderBy(x => x.Date).ToList();

      using (var context = new VetBookerContext(options))
      {
        foreach (var vetBooking in storedList)
        {
          context.Add(vetBooking);
          context.SaveChanges();
        }
      }

      // Act
      List<VetBooking> actualList;
      using (var context = new VetBookerContext(options))
      {
        var repository = new VetBookingRepository(context);
        actualList = repository.GetAll().ToList();
      }

      // Assert
      Assert.Equal(expectedList, actualList, new VetBookingEqualityComparer());
    }

    private class VetBookingEqualityComparer : IEqualityComparer<VetBooking>
    {
      public bool Equals([AllowNull] VetBooking x, [AllowNull] VetBooking y)
      {
        return x.Id == y.Id;
      }

      public int GetHashCode([DisallowNull] VetBooking obj)
      {
        return obj.Id.GetHashCode();
      }
    }

    private VetBooking CreateVetBooking(int id, DateTime dateTime)
    {
      return new VetBooking
      {
        Id = id,
        FirstName = "Paul",
        LastName = "Simons",
        Date = dateTime,
        Email = "Paul@PaulSimons.com",
        Pet = "Katinas",
        VetId = 1
      };
    }
  }
}