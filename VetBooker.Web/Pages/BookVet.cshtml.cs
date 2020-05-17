using VetBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VetBooker.Core.Processor;

namespace VetBooker.Web.Pages
{
    public class BookVetModel : PageModel
    {
        private IVetBookingRequestProcessor _vetBookingRequestProcessor;

        public BookVetModel(IVetBookingRequestProcessor vetBookingRequestProcessor)
        {
            _vetBookingRequestProcessor = vetBookingRequestProcessor;
        }

        [BindProperty]
        public VetBookingRequest VetBookingRequest { get; set; }

        public IActionResult OnPost()
        {
            IActionResult actionResult = Page();

            if (ModelState.IsValid)
            {
                var result = _vetBookingRequestProcessor.BookVet(VetBookingRequest);
                if (result.Code == VetBookingResultCode.Success)
                {
                    actionResult = RedirectToPage("BookVetConfirmation", new
                    {
                        result.VetBookingId,
                        result.FirstName,
                        result.Date,
                        result.Pet
                    });
                }

                else if (result.Code == VetBookingResultCode.NoVetAvailable)
                {
                    ModelState.AddModelError("VetBookingRequest.Date", "No Vet available for selected date");
                }

            }

            return actionResult;
        }
    }
}