using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Lib.Net.Http.WebPush;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;
using AAUG.DomainModels.ViewModels.Notification;
using Microsoft.AspNetCore.Authorization;
using AAUG.Service.Implementations.Notification;
using AAUG.Service.Interfaces.Notification;

namespace AAUG.Api.Controllers.Notification
{
    [Route("api/Notifiation/")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly PushServiceClient _pushClient;
        private readonly VapidAuthentication _vapidAuth;
        private readonly INotificationService notificationService;
        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }
        [HttpPost("SaveSubScription")]
        public async Task<IActionResult> SaveSubScription(SubscriptionViewModel model)
        {
            return Ok(await notificationService.SaveSubscriptionAsync(model));
        }
        
        [HttpPost("SendNotification")]
        [Authorize(Roles = "King,Varich,Hanxnakhumb")]
        public async Task<IActionResult> SendNotification([FromBody] NotificationPayload payload)
        {
            return Ok(await notificationService.SendNotificationAsync(payload));
        }

        [HttpGet("CountActiveSubs")]
        [Authorize(Roles = "King,Varich,Hanxnakhumb")]
        public async Task<IActionResult> CountActiveSubs()
        {
            return Ok(await notificationService.CountActiveSubsAsync());
        }

    }
}


