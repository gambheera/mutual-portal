using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mutual.Portal.Utility.Enums
{
    public enum NotificationTypes
    {
        UserWatched=1, // this notification will show if a user view my profile individually
        MutualFound=2, // Shows if a new mutual found for you
        AnotherOneFoundMutualFromYourHospital=3, // my friend in my current hospital has found a mutual

    }
}
