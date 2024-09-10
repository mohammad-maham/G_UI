using System;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }

        [Display(Name = "نام بانک")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string BankName { get; set; }

        [Display(Name = "شماره حساب")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        [RegularExpression("^([0-9])+$", ErrorMessage = "شماره حساب وارد شده معتبر نیست")]
        public string Hesab { get; set; }


        [Display(Name = "شبا")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        [RegularExpression("^([0-9]{24})$", ErrorMessage = "شبای وارد شده نامعتبر است")]
        public string Shaba { get; set; }

        [Display(Name = "شماره کارت")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        [RegularExpression("^(6037-99|6219-86|5892-10|6393-46|6276-48|6396-07|6279-61|5047-06|6037-70|5029-38|6280-23|6037-69|6277-60|6104-33|5029-08|6273-83|6274-12|5894-63|6221-06|5076-77|5022-29|6062-56|6395-99|6063-73|6274-88|5054-16|5859-83)\\d.{11}$", ErrorMessage = "شماره کارت وارد شده نامعتبر است")]
        [MaxLength(19, ErrorMessage = "بازه/طول شماره کارت نامعتبر است")]
        public string BankAccountNumber { get; set; }

        public bool ActiveCard { get; set; }
    }
}
