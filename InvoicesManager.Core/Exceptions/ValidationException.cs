using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoicesManager.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

        public ValidationException() : base("One or more validation failures have occurred.")
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures
                .Select(e => e.PropertyName)
                .Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures
                    .Where(e => e.PropertyName == propertyName)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
