using System.Collections;
using System.Collections.Generic;

namespace InvoicesManager.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public User(
            string name,
            string surname,
            string address,
            string email,
            string password)
        {
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
            Password = password;
        }
    }
}
