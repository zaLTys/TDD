using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using Moq;
using Xunit;

namespace VetBooker.Web.Pages
{
  public class VetsModelTests
  {
    [Fact]
    public void ShouldGetAllVets()
    {
      // Arrange
      var vets = new[]
      {
        new Vet(),
        new Vet(),
        new Vet(),
      };

      var vetRepositoryMock = new Mock<IVetRepository>();
      vetRepositoryMock.Setup(x => x.GetAll())
          .Returns(vets);

      var vetsModel = new VetsModel(vetRepositoryMock.Object);

      // Act
      vetsModel.OnGet();

      // Assert
      Assert.Equal(vets, vetsModel.Vets);
    }
  }
}
