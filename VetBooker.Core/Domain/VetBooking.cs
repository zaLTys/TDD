namespace VetBooker.Core.Domain
{
  public class VetBooking : VetBookingBase
  {
    public int Id { get; set; }
    public int VetId { get; set; }
  }
}