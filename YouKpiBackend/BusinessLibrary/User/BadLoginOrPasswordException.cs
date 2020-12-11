using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.BusinessLibrary.User
{
    [Serializable]

    public class BadLoginOrPasswordException : Exception
    {

        public BadLoginOrPasswordException() : base("Zły login albo hasło")
        {
        }
        public BadLoginOrPasswordException(string message) : base(message) { }
        public BadLoginOrPasswordException(string message, Exception inner) : base(message, inner) { }
        protected BadLoginOrPasswordException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
