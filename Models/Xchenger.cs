using System;
using System.Collections.Generic;

namespace G_APIs.Models
{

    public partial class Xchenger
    {
        public long Id { get; set; }

        public long WalleId { get; set; }

        public string SourceAddress { get; set; }

        public long SourceWalletCurrency { get; set; }

        public decimal SourceAmount { get; set; }

        public string DestinationAddress { get; set; }

        public long DestinationWalletCurrency { get; set; }

        public decimal DestinationAmout { get; set; }

        public DateTime XchengData { get; set; }
        public string PersianDate
        {
            get
            {
                try
                {
                    return new PersianDateTime(XchengData).ToString("yyyy/MM/dd HH:mm:ss");

                }
                catch (Exception)
                {

                    return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                }
            }
        }
    }
}
