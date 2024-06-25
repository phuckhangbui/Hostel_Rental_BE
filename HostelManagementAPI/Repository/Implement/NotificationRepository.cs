﻿using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs;
using Repository.Interface;

namespace Repository.Implement
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMapper _mapper;

        public NotificationRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> GetAllNotifications()
        {
            var list = await NotificationDao.Instance.GetAllAsync();
            return _mapper.Map<List<NotificationDto>>(list);
        }

        public async Task<NotificationDto> CreateNotification(NotificationDto notificationDto)
        {
            var notification = _mapper.Map<Notification>(notificationDto);

            await NotificationDao.Instance.CreateAsync(notification);

            return _mapper.Map<NotificationDto>(notification);
        }

        public async Task UpdateNotificationStatus(int id)
        {
            NotificationDao.Instance.UpdateNotificationToRead(id);
        }
    }
}
