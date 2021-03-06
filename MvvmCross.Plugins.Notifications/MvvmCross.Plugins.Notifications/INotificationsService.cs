﻿using System.Threading.Tasks;
using MvvmCross.Plugins.Notifications.Data;

namespace MvvmCross.Plugins.Notifications
{
	public interface INotificationsService
	{
		Task<ServiceResponse> SubscribeToNotifications();
		Task<ServiceResponse> UnsubscribeFromNotifications();
	}
}
