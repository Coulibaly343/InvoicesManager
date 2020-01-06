using System;

namespace InvoicesManager.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string Name { get; set; }

        public Invoice(string name)
        {
            Name = name;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
