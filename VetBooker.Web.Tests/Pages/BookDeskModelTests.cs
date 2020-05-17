using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using VetBooker.Core.Domain;
using VetBooker.Core.Processor;
using Xunit;

namespace VetBooker.Web.Pages
{
    public class BookVetModelTests
    {
        private Mock<IVetBookingRequestProcessor> _processorMock;
        private BookVetModel _bookVetModel;
        private VetBookingResult _vetBookingResult;

        public BookVetModelTests()
        {
            _processorMock = new Mock<IVetBookingRequestProcessor>();
            _bookVetModel = new BookVetModel(_processorMock.Object)
            {
                VetBookingRequest = new VetBookingRequest()
            };
            _vetBookingResult = new VetBookingResult()
            {
                Code = VetBookingResultCode.Success
            };

            _processorMock.Setup(x => x.BookVet(_bookVetModel.VetBookingRequest))
                .Returns(_vetBookingResult);
        }

        [Theory]
        [InlineData(1, true)]
        [InlineData(0, false)]
        public void ShouldCallBookVetMethodOfProcessorIfModelIsValid(int expectedBookVetCalls, bool isModelValid)
        {
            //Act
            if (!isModelValid)
            {
                _bookVetModel.ModelState.AddModelError("JustAKey", "AnErrorMessage");
            }
            _bookVetModel.OnPost();

            //Assert
            _processorMock.Verify(x => x.BookVet(_bookVetModel.VetBookingRequest), Times.Exactly(expectedBookVetCalls));
        }

        [Fact]
        public void ShouldAddModelErrorIfNoVetIsAvailable()
        {
            _vetBookingResult.Code = VetBookingResultCode.NoVetAvailable;
            //Act
            _bookVetModel.OnPost(); 
            //Assert
            var modelStateEntry = Assert.Contains("VetBookingRequest.Date", _bookVetModel.ModelState);
            var modelError = Assert.Single(modelStateEntry.Errors);
            Assert.Equal("No Vet available for selected date", modelError.ErrorMessage);
        }

        [Fact]
        public void ShouldNotAddModelErrorIfVetIsAvailable()
        {
            _vetBookingResult.Code = VetBookingResultCode.Success;
            //Act
            _bookVetModel.OnPost();
            //Assert
            Assert.DoesNotContain("VetBookingRequest.Date", _bookVetModel.ModelState);
        }

        [Theory]
        [InlineData(typeof(PageResult), false, null)]
        [InlineData(typeof(PageResult), true, VetBookingResultCode.NoVetAvailable)]
        [InlineData(typeof(RedirectToPageResult), true, VetBookingResultCode.Success)]
        public void ShouldReturnExpectedActionResult(Type expectedActionResultType, bool isModelValid,
            VetBookingResultCode? vetBookingResultCode)
        {
            //Arrange
            if (!isModelValid)
            {
                _bookVetModel.ModelState.AddModelError("JustAKey","AnErrorMessage");
            }
            
            if (vetBookingResultCode.HasValue)
            {
                _vetBookingResult.Code = vetBookingResultCode.Value;
            }
            //Act
            IActionResult actionResult = _bookVetModel.OnPost();
            //Assert

            Assert.IsType(expectedActionResultType, actionResult);
        }

        [Fact]
        public void ShouldRedirectToBookVetConfirmationPage()
        {
            //Arrange
            _vetBookingResult.Code = VetBookingResultCode.Success;
            _vetBookingResult.VetBookingId = 17;
            _vetBookingResult.FirstName = "Povilas";
            _vetBookingResult.Pet = "Katinas";
            _vetBookingResult.Date = new DateTime(2020,5,17);

            //Act
            IActionResult actionResult = _bookVetModel.OnPost();
            
            //Assert
            var redirectToPageResult = Assert.IsType<RedirectToPageResult>(actionResult);
            Assert.Equal("BookVetConfirmation", redirectToPageResult.PageName);

            //var routeValues = redirectToPageResult.RouteValues;
            IDictionary<string,object> routeValues = redirectToPageResult.RouteValues;
            Assert.Equal(4, routeValues.Count);

            var vetBookingId = Assert.Contains("VetBookingId", routeValues);
            Assert.Equal(_vetBookingResult.VetBookingId, vetBookingId);
            var vetBookingFirstName = Assert.Contains("FirstName", routeValues);
            Assert.Equal(_vetBookingResult.FirstName, vetBookingFirstName);
            var vetBookingDate = Assert.Contains("Date", routeValues);
            Assert.Equal(_vetBookingResult.Date, vetBookingDate);
            var vetBookingPet = Assert.Contains("Pet", routeValues);
            Assert.Equal(_vetBookingResult.Pet, vetBookingPet);
        }
    }
}
 