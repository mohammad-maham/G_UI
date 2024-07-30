﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G_APIs.Common { 

public class Enums
{

    public enum GoldHost
    {
        Accounting,
        IPG,
        Store,
        Wallet,
    }

    public enum TransactionType
    {
        Deposit = 4,
        Windrow = 3,
        Sell = 1,
        Buy = 2
    }
    public enum TransactionMode
    {
        Offline = 1,
        Online= 2,
    }
    public enum Unit
    {
        Rial = 1,
        Gram = 2,
    }
    public enum Currency
    {
        Money = 1,
        Gold = 2,
    }
}}