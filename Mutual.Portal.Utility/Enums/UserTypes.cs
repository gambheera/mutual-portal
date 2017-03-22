using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutual.Portal.Utility.Enums
{
    public enum UserStates
    {
        Free = 1,
        Registered = 2,
        BlackListed = 3, // This is happening because of unethical behaviour
        Disconnected = 4, // This is done by the user
        Suspended = 5, // This is for long term unused accounts
    }

    public enum EmploymentTypes
    {
        Nursing = 1,
        Teaching = 3,
    }

    public enum UserAuthenticationType
    {
        Google = 1,
        Facebook = 2
    }
}
