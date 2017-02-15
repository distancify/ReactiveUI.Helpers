using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUI.Helpers
{
    /// <summary>
    /// Exception thrown when the service locator is unable to resolve a constructor parameter
    /// </summary>
    public class ResolveException : Exception
    {
        public ResolveException(string message) : base(message)
        {
        }
    }
}
