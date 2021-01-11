using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.BusinessLibrary.Production
{
    public class StepAlreadyClosedException : Exception
    {
        public StepAlreadyClosedException(string message) : base(message)
        {
    }
        public StepAlreadyClosedException() : base("Step has been already closed")
        {
        }
    }
}
