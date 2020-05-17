using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace VetBooker.Web.Pages
{
  public class BookVetConfirmationModel : PageModel
  {
    public int VetBookingId { get; set; }

    public string FirstName { get; set; }
    public string Pet { get; set; }

    public DateTime Date { get; set; }

    public void OnGet(int vetBookingId, string firstName, DateTime date, string pet)
    {
      VetBookingId = vetBookingId;
      FirstName = firstName;
      Pet = pet;
      Date = date;
    }
  }
}