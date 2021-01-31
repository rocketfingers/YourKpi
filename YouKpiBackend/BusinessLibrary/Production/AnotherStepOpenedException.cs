using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.BusinessLibrary.Production
{
    public class AnotherStepOpenedException : Exception
    {
        public string StepOpenedId { get; internal set; }
        public AnotherStepOpenedException(string message, string stepOpenedId) : base(message)
        {
            this.StepOpenedId = stepOpenedId;
        }
        public AnotherStepOpenedException(string stepOpenedId) : base("Another step opened by user")
        {
            this.StepOpenedId = stepOpenedId;
        }
    }

}
