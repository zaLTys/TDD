using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using Moq;
using Xunit;

namespace VetBooker.Web.Pages
{
  public class VetBookingsModelTests
  {
    [Fact]
    public void ShouldGetAllVetBookings()
    {
      // Arrange
      var vetBookings = new[]
      {
        new VetBooking(),
        new VetBooking(),
        new VetBooking(),
      };

      var vetBookingRepositoryMock = new Mock<IVetBookingRepository>();
      vetBookingRepositoryMock.Setup(x => x.GetAll())
        .Returns(vetBookings);

      var vetBookingsModel = new VetBookingsModel(vetBookingRepositoryMock.Object);

      // Act
      vetBookingsModel.OnGet();

      // Assert
      Assert.Equal(vetBookings, vetBookingsModel.VetBookings);
    }
  }
}
