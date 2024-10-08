﻿using System;
using System.Collections.Generic;

namespace G_APIs.Models
{
    public partial class Wallet
    {
        public long Id { get; set; }

        public long? UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public short Status { get; set; }
        public bool ShowButtons { get; set; } = true;

    }
}
