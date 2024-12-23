using System;
using System.Collections.Generic;

namespace DataAccess.BussinessObjects;

public partial class Airport
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? City { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Flight> FlightArrivingAirportNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightDepartingAirportNavigations { get; set; } = new List<Flight>();
}
