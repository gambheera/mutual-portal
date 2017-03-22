using Mutual.Portal.Core.Entities.Common;
using Mutual.Portal.Service.BusinessLogic.UserManagement.Dto;
using System;

namespace Mutual.Portal.Service.BusinessLogic.NotificationManagement.Dto
{
    public class NotificationDto
    {
        #region Public Properties

        public int Id { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public bool IsRead { get; set; }
        public int Type { get; set; }
        public DateTime Time { get; set; }
        public UserDto User { get; set; }

        #endregion

        public static NotificationDto GetDto(Notification notification)
        {
            var obj = new NotificationDto()
            {
                Id = notification.Id,
                Header = notification.Header,
                Body = notification.Body,
                IsRead = notification.IsRead,
                Time = notification.Time,
                Type = notification.Type,
                User = notification.User != null ? UserDto.GetDto(notification.User) : null
            };

            return obj;
        }

        public static Notification GetBo(NotificationDto notificationDto, User user)
        {
            var obj = new Notification()
            {
                Id = notificationDto.Id,
                Header = notificationDto.Header,
                Body = notificationDto.Body,
                IsRead = notificationDto.IsRead,
                Time = notificationDto.Time,
                Type = notificationDto.Type,
                User = user
            };

            return obj;
        }
    }
}
