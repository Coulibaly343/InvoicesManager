using System.Collections.Generic;

namespace InvoicesManager.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Address { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public ICollection<Invoice> Invoices { get; private set; }
       
        public User()
        {

        }

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
            CreatePasswordHash(password);
        }

        private void CreatePasswordHash(string password)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
