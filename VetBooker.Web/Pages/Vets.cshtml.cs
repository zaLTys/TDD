using System.Collections.Generic;
using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VetBooker.Web.Pages
{
  public class VetsModel : PageModel
  {
    private readonly IVetRepository _vetRepository;

    public VetsModel(IVetRepository vetRepository)
    {
      _vetRepository = vetRepository;
    }

    public IEnumerable<Vet> Vets { get; set; }

    public void OnGet()
    {
      Vets = _vetRepository.GetAll();
    }
  }
}