﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{

    public class BankAccount
    {
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
        [ReadOnly(true)]
        public string BankAccountNumber { get; set; }

        public bool ActiveCard { get; set; }
    }
}
