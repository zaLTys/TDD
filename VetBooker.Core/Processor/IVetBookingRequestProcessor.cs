using VetBooker.Core.Domain;

namespace VetBooker.Core.Processor
{
    public interface IVetBookingRequestProcessor
    {
        VetBookingResult BookVet(VetBookingRequest request);
    }
}