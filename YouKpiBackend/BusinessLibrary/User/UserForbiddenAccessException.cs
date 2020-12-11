using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouKpiBackend.BusinessLibrary.User
{
    [Serializable]

    public class UserForbiddenAccessException : Exception
    {
        public UserForbiddenAccessException() : base("Nie masz dostępu do tego miejsca")
        {
        }
        public UserForbiddenAccessException(string message) : base(message) { }
        public UserForbiddenAccessException(string message, Exception inner) : base(message, inner) { }
        protected UserForbiddenAccessException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
