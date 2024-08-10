using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace G_APIs.Models
{

    public partial class WalletBankAccount
    {
        public long Id { get; set; }

        public long WalletId { get; set; }

        public int BankId { get; set; }
        public string LogoPath { get; set; }


        public int RegionId { get; set; }

        public short Status { get; set; }

        public DateTime RegDate { get; set; }

        public short OrderId { get; set; }

        public string ValidationInfo { get; set; }


        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Name { get; set; }

        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string BankName { get; set; }


        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Hesab { get; set; }


        [Display(Name = "شبا")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Shaba { get; set; }

        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string BankAccountNumber { get; set; }

        public bool ActiveCard { get; set; }
    }
}
