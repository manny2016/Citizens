using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public abstract class CitizensException : Exception
    {
        public CitizensException() { }

        public CitizensException(string message)
            : base(message) { }
        
        public CitizensException(string message, Exception innerException)
            : base(message, innerException) { }

        
    }
}
