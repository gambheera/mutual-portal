

using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Utility.Models;

namespace Mutual.Portal.Service.BusinessLogic.NotificationManagement
{
    public interface INotificationManager
    {
        ResponseObject GetUserNotificationList(string requesterGuid);
        ResponseObject SaveUserNotification(Notification notification);
        ResponseObject DeleteNotification(int notificationId);
        ResponseObject MarkAsReadNotification(int notificationId);
    }
}
