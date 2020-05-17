using VetBooker.Core.Domain;
using System;
using System.Collections.Generic;

namespace VetBooker.Core.DataInterface
{
  public interface IVetRepository
  {
    IEnumerable<Vet> GetAvailableVets(DateTime date);
    IEnumerable<Vet> GetAll();
  }
}
