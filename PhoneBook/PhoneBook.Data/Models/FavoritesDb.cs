using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneBook.Data.Models
{
    public class FavoritesDb
    {
        public long Id { get; set; }

        public string Login { get; set; }

        public EmployeeDb WorkerDb { get; set; }
    }
}
