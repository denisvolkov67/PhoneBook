using System;

namespace PhoneBook.Logic.Models
{
    public class User : IComparable
    {
        public string Login { get; set; }

        public string DisplayName { get; set; }

        public string Surname { get; set; }

        public string GivenName { get; set; }

        public string Description { get; set; }

        public string Office { get; set; }

        public string Telephone { get; set; }

        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public int CompareTo(object o)
        {
            User u = o as User;
            if (u != null)
                return this.DisplayName.CompareTo(u.DisplayName);
            else
                throw new Exception("Невозможно сравнить два объекта");
        }

    }
}
