using VetBooker.Core.Domain;
using System.Collections.Generic;

namespace VetBooker.Core.DataInterface
{
  public interface IVetBookingRepository
  {
    void Save(VetBooking vetBooking);
    IEnumerable<VetBooking> GetAll();
  }
}
