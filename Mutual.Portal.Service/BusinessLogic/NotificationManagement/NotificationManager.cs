using System;
using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Utility.Models;
using Mutual.Portal.Core.Persistence;

namespace Mutual.Portal.Service.BusinessLogic.NotificationManagement
{
    public class NotificationManager : INotificationManager
    {
        private IOperationDbContext _operationContext;

        public NotificationManager(IOperationDbContext operationContext)
        {
            _operationContext = operationContext;
        }

        public ResponseObject DeleteNotification(int notificationId)
        {
            throw new NotImplementedException();
        }

        public ResponseObject GetUserNotificationList(string requesterGuid)
        {
            throw new NotImplementedException();
        }

        public ResponseObject MarkAsReadNotification(int notificationId)
        {
            throw new NotImplementedException();
        }

        public ResponseObject SaveUserNotification(Notification notification)
        {
            throw new NotImplementedException();
        }
    }
}
