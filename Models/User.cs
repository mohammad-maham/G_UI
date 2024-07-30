using System;

namespace G_APIs.Models
{

    [Serializable]
    public class User
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string @Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public string Captcha { get; set; }
        public long? OTP { get; set; }
        public string Role { get; set; }

        public string Family { get; set; }
        public string Father { get; set; }
        public string Address { get; set; }
        public byte Gender { get; set; }
        public string BirthDate { get; set; }
        public DateTime RegDate { get; set; }

        public short Status { get; set; }

        public object Otpinfo { get; set; }


    }
}