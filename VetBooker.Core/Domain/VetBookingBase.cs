using VetBooker.Core.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace VetBooker.Core.Domain
{
  public class VetBookingBase
  {
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string Email { get; set; }   
    
    [Required]
    [StringLength(100)]
    public string Pet { get; set; }

    [DataType(DataType.Date)]
    [DateInFuture]
    [DateWithoutTime]
    public DateTime Date { get; set; }
  }
}