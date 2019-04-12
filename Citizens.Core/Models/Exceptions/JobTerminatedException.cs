using System;
using System.Collections.Generic;
using System.Text;

namespace Citizens.Core
{
    public class JobTerminatedException : CitizensException
    {
        public override string Message
        {
            get
            {
                return "Job is terminated.";
            }
        }
    }
}
