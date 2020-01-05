using System;

namespace InvoicesManager.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }

        public NotFoundException(string name) : base($"Entity \"{name}\" collection not found.")
        {
        }
    }
}
