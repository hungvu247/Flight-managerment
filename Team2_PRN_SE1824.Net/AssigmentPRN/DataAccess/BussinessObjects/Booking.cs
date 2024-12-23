using System;
using System.Collections.Generic;

namespace DataAccess.BussinessObjects;

public partial class Booking
{
    public int Id { get; set; }

    public int? PassengerId { get; set; }

    public int? FlightId { get; set; }

    public int? BookingPlatformId { get; set; }

    public DateTime? BookingTime { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Baggage> Baggages { get; set; } = new List<Baggage>();

    public virtual BookingPlatform? BookingPlatform { get; set; }

    public virtual Flight? Flight { get; set; }

    public virtual Passenger? Passenger { get; set; }
}
