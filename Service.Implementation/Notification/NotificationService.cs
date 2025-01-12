using System.Collections;
using System.Net;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels.Notification;
using AAUG.Service.Interfaces.Notification;
using AutoMapper;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AAUG.Service.Implementations.Notification;

public class NotificationService : INotificationService
{
    private IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly Lib.Net.Http.WebPush.PushServiceClient _pushClient;

    public NotificationService(IAaugUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;

        _pushClient = new Lib.Net.Http.WebPush.PushServiceClient
        {
            DefaultAuthentication = new VapidAuthentication(
                configuration["VapidKeys:PublicKey"],
                configuration["VapidKeys:PrivateKey"])
            {
                Subject = configuration["VapidKeys:Subject"]
            }
        };
    }
    public async Task<bool> SaveSubscriptionAsync(SubscriptionViewModel model)
    {
        var data = new PushSubscription
        {
            EndPoint = model.Endpoint,
            AuthKey = model.Keys.Auth,
            P256dhKey = model.Keys.P256dh
        };
        var dataDto = new NotificationInsertDto
        {
            Endpoint = data.EndPoint,
            Auth = data.AuthKey,
            P256dh = data.P256dhKey,
            IsActive = true,
        };
        await unitOfWork.PushSubscriptionRepository.AddDataAsync(dataDto);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<bool> SendNotificationAsync(NotificationPayload payload)
    {
        var subscriptions = await unitOfWork.PushSubscriptionRepository.GetAllAsync().ToListAsync();
        var activeSubs = subscriptions.Where(a => a.IsActive == true);
        if (!activeSubs.Any())
            return false;
        bool isSuccess = true; // Track success status

        foreach (var subscription in activeSubs)
        {
            var pushSubscription = new Lib.Net.Http.WebPush.PushSubscription
            {
                Endpoint = subscription.EndPoint,
                Keys = new Dictionary<string, string>
                {
                    { "p256dh", subscription.P256dhKey },
                    { "auth", subscription.AuthKey }
                }
            };

            // Construct JSON payload for the notification
            string payloadJson = $@"
            {{
                ""title"": ""{payload.Title}"",
                ""body"": ""{payload.Body}"",
                ""icon"": ""{payload.Icon}"",
                ""url"": ""{payload.Url}""
            }}";

            var pushMessage = new Lib.Net.Http.WebPush.PushMessage(payloadJson);

            try
            {
                // Attempt to send the push notification
                await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, pushMessage);
            }
            catch (Lib.Net.Http.WebPush.PushServiceClientException ex) when (ex.StatusCode == HttpStatusCode.Gone)
            {
                // Log specific errors (e.g., invalid subscription)
                // Console.WriteLine($"Push notification failed for {subscription.EndPoint}: {ex.Message}");

                subscription.IsActive = false;
                await unitOfWork.SaveChangesAsync();
                await unitOfWork.CommitTransactionAsync();

                isSuccess = false;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
        }

        return isSuccess;
    }

    #region count active subs
    public async Task<int> CountActiveSubsAsync()
    {
        var data = await unitOfWork.PushSubscriptionRepository.GetAllAsync().ToListAsync();
        var activeData = data.Where(a => a.IsActive == true);

        return activeData.Count();
    }
    #endregion


}