using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IPushSubscriptionRepository
{
    Task DeleteRecordAsync(int id);
    IQueryable<PushSubscription> GetAllAsync();
    IQueryable<PushSubscription> GetByEndpointAsync(string endpoint);
    Task AddDataAsync(NotificationInsertDto entity);
}