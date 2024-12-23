using System;
using System.Collections.Generic;

namespace DataAccess.BussinessObjects;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string? PassWord { get; set; }

    public string? MemberId { get; set; }

    public bool? Status { get; set; }
}
