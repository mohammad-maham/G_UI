using System;
using System.ComponentModel.DataAnnotations;

namespace G_APIs.Models
{
    [Serializable]
    public class User
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        [Required, Display(Name = "نام کاربری/کد ملی")]
        public string ForgotUsername { get; set; }
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string PasswordRepeat { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string @Name { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Email { get; set; }
        [Display(Name = "تلفن")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Phone { get; set; }
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Mobile { get; set; }
        [Display(Name = "کد ملی"), Required(ErrorMessage = "وارد کردن این فیلد الزامیست")]
        public string NationalCode { get; set; }
        public string Captcha { get; set; }
        public long? OTP { get; set; }
        public string Role { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Family { get; set; }
        [Display(Name = "نام پدر")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Father { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string Address { get; set; }
        public byte Gender { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "وارد کردن این فیلد الزامیست  ")]
        public string BirthDate { get; set; }
        public DateTime RegDate { get; set; }
        public short Status { get; set; }
        public object Otpinfo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string FatherName { get; set; }
        public string SedadInfo { get; set; }
        public string NationalCardImage { get; set; }

        public double    OnlinePrice { get; set; }
    }
}