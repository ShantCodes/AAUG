using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels.Notification;

namespace AAUG.Service.Interfaces.Notification;

public interface INotificationService
{
    Task<bool> SaveSubscriptionAsync(SubscriptionViewModel model);
    Task<bool> SendNotificationAsync(NotificationPayload payload);
    Task<int> CountActiveSubsAsync();
}