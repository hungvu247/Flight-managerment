using System;
using System.Collections.Generic;

namespace DataAccess.BussinessObjects;

public partial class Baggage
{
    public int Id { get; set; }

    public int? BookingId { get; set; }

    public decimal? WeightInKg { get; set; }

    public bool? Status { get; set; }

    public virtual Booking? Booking { get; set; }
}
