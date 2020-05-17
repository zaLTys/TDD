using VetBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VetBooker.Web.Pages
{
  public class BookVetModel : PageModel
  {
    [BindProperty]
    public VetBookingRequest VetBookingRequest { get; set; }

    public void OnPost()
    {
      
    }
  }
}