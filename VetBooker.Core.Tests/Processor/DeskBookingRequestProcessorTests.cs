using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace VetBooker.Core.Processor
{
  public class VetBookingRequestProcessorTests
  {
    private readonly VetBookingRequest _request;
    private readonly List<Vet> _availableVets;
    private readonly Mock<IVetBookingRepository> _vetBookingRepositoryMock;
    private readonly Mock<IVetRepository> _vetRepositoryMock;
    private readonly VetBookingRequestProcessor _processor;

    public VetBookingRequestProcessorTests()
    {
      _request = new VetBookingRequest
      {
        FirstName = "Paul",
        LastName = "Simons",
        Email = "Paul@PaulSimons.com",
        Pet = "Katinas",
        Date = new DateTime(2020, 1, 28)
      };

      _availableVets = new List<Vet> { new Vet { Id = 7 } };

      _vetBookingRepositoryMock = new Mock<IVetBookingRepository>();
      _vetRepositoryMock = new Mock<IVetRepository>();
      _vetRepositoryMock.Setup(x => x.GetAvailableVets(_request.Date))
        .Returns(_availableVets);

      _processor = new VetBookingRequestProcessor(
        _vetBookingRepositoryMock.Object, _vetRepositoryMock.Object);
    }

    [Fact]
    public void ShouldReturnVetBookingResultWithRequestValues()
    {
      // Act
      VetBookingResult result = _processor.BookVet(_request);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(_request.FirstName, result.FirstName);
      Assert.Equal(_request.LastName, result.LastName);
      Assert.Equal(_request.Email, result.Email);
      Assert.Equal(_request.Pet, result.Pet);
      Assert.Equal(_request.Date, result.Date);
    }

    [Fact]
    public void ShouldThrowExceptionIfRequestIsNull()
    {
      var exception = Assert.Throws<ArgumentNullException>(() => _processor.BookVet(null));

      Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void ShouldSaveVetBooking()
    {
      VetBooking savedVetBooking = null;
      _vetBookingRepositoryMock.Setup(x => x.Save(It.IsAny<VetBooking>()))
        .Callback<VetBooking>(vetBooking =>
        {
          savedVetBooking = vetBooking;
        });

      _processor.BookVet(_request);

      _vetBookingRepositoryMock.Verify(x => x.Save(It.IsAny<VetBooking>()), Times.Once);

      Assert.NotNull(savedVetBooking);
      Assert.Equal(_request.FirstName, savedVetBooking.FirstName);
      Assert.Equal(_request.LastName, savedVetBooking.LastName);
      Assert.Equal(_request.Email, savedVetBooking.Email);
      Assert.Equal(_request.Pet, savedVetBooking.Pet);
      Assert.Equal(_request.Date, savedVetBooking.Date);
      Assert.Equal(_availableVets.First().Id, savedVetBooking.VetId);
    }

    [Fact]
    public void ShouldNotSaveVetBookingIfNoVetIsAvailable()
    {
      _availableVets.Clear();

      _processor.BookVet(_request);

      _vetBookingRepositoryMock.Verify(x => x.Save(It.IsAny<VetBooking>()), Times.Never);
    }

    [Theory]
    [InlineData(VetBookingResultCode.Success, true)]
    [InlineData(VetBookingResultCode.NoVetAvailable, false)]
    public void ShouldReturnExpectedResultCode(
      VetBookingResultCode expectedResultCode, bool isVetAvailable)
    {
      if (!isVetAvailable)
      {
        _availableVets.Clear();
      }

      var result = _processor.BookVet(_request);

      Assert.Equal(expectedResultCode, result.Code);
    }

    [Theory]
    [InlineData(5, true)]
    [InlineData(null, false)]
    public void ShouldReturnExpectedVetBookingId(
      int? expectedVetBookingId, bool isVetAvailable)
    {
      if (!isVetAvailable)
      {
        _availableVets.Clear();
      }
      else
      {
        _vetBookingRepositoryMock.Setup(x => x.Save(It.IsAny<VetBooking>()))
          .Callback<VetBooking>(vetBooking =>
          {
            vetBooking.Id = expectedVetBookingId.Value;
          });
      }

      var result = _processor.BookVet(_request);

      Assert.Equal(expectedVetBookingId, result.VetBookingId);
    }
  }
}
