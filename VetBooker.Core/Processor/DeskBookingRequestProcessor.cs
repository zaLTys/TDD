using VetBooker.Core.DataInterface;
using VetBooker.Core.Domain;
using System;
using System.Linq;

namespace VetBooker.Core.Processor
{
    public class VetBookingRequestProcessor : IVetBookingRequestProcessor
    {
    private readonly IVetBookingRepository _vetBookingRepository;
    private readonly IVetRepository _vetRepository;

    public VetBookingRequestProcessor(IVetBookingRepository vetBookingRepository,
      IVetRepository vetRepository)
    {
      _vetBookingRepository = vetBookingRepository;
      _vetRepository = vetRepository;
    }

    public VetBookingResult BookVet(VetBookingRequest request)
    {
      if (request == null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      var result = Create<VetBookingResult>(request);

      var availableVets = _vetRepository.GetAvailableVets(request.Date);
      if (availableVets.FirstOrDefault() is Vet availableVet)
      {
        var vetBooking = Create<VetBooking>(request);
        vetBooking.VetId = availableVet.Id;

        _vetBookingRepository.Save(vetBooking);

        result.VetBookingId = vetBooking.Id;
        result.Code = VetBookingResultCode.Success;
      }
      else
      {
        result.Code = VetBookingResultCode.NoVetAvailable;
      }

      return result;
    }

    private static T Create<T>(VetBookingRequest request) where T : VetBookingBase, new()
    {
      return new T
      {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Email = request.Email,
        Pet = request.Pet,
        Date = request.Date
      };
    }
  }
}