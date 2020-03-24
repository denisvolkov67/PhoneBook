using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Models
{
    public class EmployeeDb
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Office { get; set; }

        public string Email { get; set; }

        public string DepartmentDbId { get; set; }
    }
}
