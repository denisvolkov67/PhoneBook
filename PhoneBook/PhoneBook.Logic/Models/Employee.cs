using System;

namespace PhoneBook.Logic.Models
{
    public class Employee
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Telephone { get; set; }

        public string Mobile { get; set; }

        public string Office { get; set; }

        public string DepartmentId { get; set; }

        public bool Favorites { get; set; }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Employee e = (Employee)obj;
                return (Id == e.Id);
            }
        }
    }
}
