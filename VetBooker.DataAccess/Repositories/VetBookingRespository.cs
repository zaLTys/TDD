using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace VetBooker.DataAccess.Repositories
{
  public class VetBookingRepository : IVetBookingRepository
  {
    private readonly VetBookerContext _context;

    public VetBookingRepository(VetBookerContext context)
    {
      _context = context;
    }

    public IEnumerable<VetBooking> GetAll()
    {
      return _context.VetBooking.OrderBy(x => x.Date).ToList();
    }

    public void Save(VetBooking vetBooking)
    {
      _context.VetBooking.Add(vetBooking);
      _context.SaveChanges();
    }
  }
}