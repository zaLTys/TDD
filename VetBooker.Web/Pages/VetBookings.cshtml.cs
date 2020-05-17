using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace VetBooker.Web.Pages
{
  public class VetBookingsModel : PageModel
  {
    private readonly IVetBookingRepository _vetBookingRepository;

    public VetBookingsModel(IVetBookingRepository vetBookingRepository)
    {
      _vetBookingRepository = vetBookingRepository;
    }

    public IEnumerable<VetBooking> VetBookings { get; set; }

    public void OnGet()
    {
      VetBookings = _vetBookingRepository.GetAll();
    }
  }
}