using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gcm.Client;
using MvvmCross.Droid.Platform;
using MvvmCross.Platform;

namespace MvvmCross.Plugins.Notifications.Droid
{
	public abstract class MvxGcmService : GcmServiceBase
	{
		protected MvxGcmService()
		{
		}

		protected MvxGcmService(params string[] senderIds) : base(senderIds)
		{
		}

		protected override void OnRegistered(Context context, string registrationId)
		{
			GetNotificationsService().NotifyThatRegistrationSucceed(registrationId);
		}

		protected override void OnUnRegistered(Context context, string registrationId)
		{
			GetNotificationsService().NotifyThatUnregistrationSucceed();
		}

		protected override void OnHandleIntent(Intent intent)
		{
			var setup = MvxAndroidSetupSingleton.EnsureSingletonAvailable(this.ApplicationContext);
			setup.EnsureInitialized();

			base.OnHandleIntent(intent);
		}

		protected override void OnError(Context context, string errorId)
		{
			var notificationsService = GetNotificationsService();

			// todo: create push specific exception
			notificationsService.NotifyThatRegistrationFailed(new InvalidOperationException(errorId));
			notificationsService.NotifyThatUnregistrationFailed(new InvalidOperationException(errorId));
		}

		protected GcmBackendDrivenPushNotificationService GetNotificationsService()
		{
			var notificationsService = Mvx.Resolve<INotificationsService>() as GcmBackendDrivenPushNotificationService;
			if (notificationsService == null)
				throw new InvalidOperationException(
					$"{nameof(GcmBackendDrivenPushNotificationService)} is not registered for type: {nameof(INotificationsService)}");
			return notificationsService;
		}
	}
}