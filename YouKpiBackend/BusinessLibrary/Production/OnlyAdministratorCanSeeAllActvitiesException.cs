using System;
using System.Runtime.Serialization;

namespace YouKpiBackend.BusinessLibrary.Production
{
    [Serializable]
    internal class OnlyAdministratorCanSeeAllActvitiesException : Exception
    {

        public OnlyAdministratorCanSeeAllActvitiesException(string message) : base(message)
        {
        }

        public OnlyAdministratorCanSeeAllActvitiesException() : base("Only administrator can see all activities")
        {
        }

    }
}