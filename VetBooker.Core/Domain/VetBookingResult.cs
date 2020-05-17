namespace VetBooker.Core.Domain
{
  public class VetBookingResult : VetBookingBase
  {
    public VetBookingResultCode Code { get; set; }
    public int? VetBookingId { get; set; }
  }
}