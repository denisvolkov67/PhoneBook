using System;

namespace PhoneBook.Logic.Models
{
    public class User
    {
        public string Login { get; set; }

        public string DisplayName { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public string Description { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string NamePC { get; set; }

        public string LastActivity { get; set; }

        public string LastPasswordSet { get; set; }

        public DateTime? AccountExpiries { get; set; }

        public string OU { get; set; }

        public bool ExpirePassword { get; set; }

        public bool UserCannotChangePassword { get; set; }

        public bool PasswordNeverExpires { get; set; }

        public bool EnableAccount { get; set; }

        public string LastLogon { get; set; }
    }
}
