using System;
using System.Collections.Generic;

namespace G_APIs.Models { 

public partial class Currency
{
    public short Id { get; set; }

    public string Name { get; set; } 

    public string Description { get; set; }

    public short Status { get; set; }

    public short UnitId { get; set; }
}
}
