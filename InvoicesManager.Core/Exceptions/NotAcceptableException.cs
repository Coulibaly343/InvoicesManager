using System;
using System.Collections.Generic;
using System.Text;

namespace InvoicesManager.Core.Exceptions
{
    public class NotAcceptableException : Exception
    {
        public NotAcceptableException() : base("Request can not be executed.")
        {
        }
    }
}
