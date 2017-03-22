﻿using Mutual.Portal.Core.Entities.Common;
using System;

namespace Mutual.Portal.Service.BusinessLogic.UserManagement.Dto
{
    public class UserDto
    {
        #region Public Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string FacebookId { get; set; }
        public string GoogleId { get; set; }
        public DateTime RegisteredOn { get; set; }
        public string RegisteredOnString => RegisteredOn.ToString("d");
        public DateTime LastLoginOn { get; set; }
        public string LastLoginOnString => LastLoginOn.ToString("d");
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public int EmploymentType { get; set; } // Referring EmploymentTypes enum
        public int State { get; set; } // Referring UserStates enum
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Guid { get; set; }
        public int MyCurrentViewCount { get; set; }
        public int MyTotalViewCount { get; set; }

        #endregion

        public static UserDto GetDto(User user)
        {
            var obj = new UserDto()
            {
                Id = user.Id,
                ContactNumber1 = user.ContactNumber1,
                ContactNumber2 = user.ContactNumber2,
                Email = user.Email,
                EmploymentType = user.EmploymentType,
                FacebookId = user.FacebookId,
                GoogleId = user.GoogleId,
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted,
                LastLoginOn = user.LastLoginOn,
                Name = user.Name,
                RegisteredOn = user.RegisteredOn,
                State = user.State,
                Guid = user.Guid.ToString(),
                MyCurrentViewCount = user.MyCurrentViewCount,
                MyTotalViewCount = user.MyTotalViewCount
            };

            return obj;
        }

        public static User GetBo(UserDto userDto)
        {
            var obj = new User()
            {
                Id = userDto.Id,
                ContactNumber1 = userDto.ContactNumber1,
                ContactNumber2 = userDto.ContactNumber2,
                Email = userDto.Email,
                EmploymentType = userDto.EmploymentType,
                FacebookId = userDto.FacebookId,
                GoogleId = userDto.GoogleId,
                IsActive = userDto.IsActive,
                IsDeleted = userDto.IsDeleted,
                LastLoginOn = userDto.LastLoginOn,
                Name = userDto.Name,
                RegisteredOn = userDto.RegisteredOn,
                State = userDto.State,
                MyCurrentViewCount = userDto.MyCurrentViewCount,
                MyTotalViewCount = userDto.MyTotalViewCount
            };

            return obj;
        }
    }
}