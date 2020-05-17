using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VetBooker.DataAccess.Repositories
{
  public class VetRepository : IVetRepository
  {
    private readonly VetBookerContext _context;

    public VetRepository(VetBookerContext context)
    {
      _context = context;
    }

    public IEnumerable<Vet> GetAll()
    {
      return _context.Vet.ToList();
    }

    public IEnumerable<Vet> GetAvailableVets(DateTime date)
    {
      var bookedVetIds = _context.VetBooking.
        Where(x => x.Date == date)
        .Select(b => b.VetId)
        .ToList();

      return _context.Vet
        .Where(x => !bookedVetIds.Contains(x.Id))
        .ToList();
    }
  }
}