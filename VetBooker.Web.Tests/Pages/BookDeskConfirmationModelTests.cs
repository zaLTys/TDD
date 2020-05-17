using System;
using Xunit;

namespace VetBooker.Web.Pages
{
  public class BookVetConfirmationModelTests
  {
    [Fact]
    public void ShouldStoreParameterValuesInProperties()
    {
      // Arrange
      const int vetBookingId = 7;
      const string firstName = "Paul";
      const string pet = "Katinas";
      var date = new DateTime(2020, 1, 28);

      var bookVetConfirmationModel = new BookVetConfirmationModel();

      // Act
      bookVetConfirmationModel.OnGet(vetBookingId, firstName, date, pet);

      // Assert
      Assert.Equal(vetBookingId, bookVetConfirmationModel.VetBookingId);
      Assert.Equal(firstName, bookVetConfirmationModel.FirstName);
      Assert.Equal(pet, bookVetConfirmationModel.Pet);
      Assert.Equal(date, bookVetConfirmationModel.Date);
    }
  }
}
