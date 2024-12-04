using Lib.Net.Http.WebPush;

namespace AAUG.DomainModels.ViewModels.Notification;

public class PushNotificationRequest
{
    public PushSubscription Subscription { get; set; }
    public NotificationPayload Notification { get; set; }
}

public class SubscriptionViewModel
{
    public string Endpoint { get; set; }
    public SubscriptionKeys Keys { get; set; }
}

public class SubscriptionKeys
{
    public string P256dh { get; set; }
    public string Auth { get; set; }
}

public class NotificationPayload
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string Icon { get; set; }
    public string Url { get; set; }
}
