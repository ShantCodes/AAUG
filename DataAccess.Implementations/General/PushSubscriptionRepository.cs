using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.General;

public class PushSubscriptionRepository : EntityRepository<PushSubscription>, IPushSubscriptionRepository
{
    private IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public PushSubscriptionRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    //public async Task AddDataAsync(PushSubscription pushSubscription)
    //{
    //    await AddAsync(pushSubscription);
    //}

    public async Task AddDataAsync(NotificationInsertDto entity)
    {
        var data = new PushSubscription
        {
            EndPoint = entity.Endpoint,
            P256dhKey = entity.P256dh,
            AuthKey = entity.Auth,
            IsActive = entity.IsActive,
        };
        await AddAsync(data);
    }

    public IQueryable<PushSubscription> GetByEndpointAsync(string endpoint)
    {
        return GetData(a => a.EndPoint == endpoint);
    }

    public IQueryable<PushSubscription> GetAllAsync()
    {
        return GetData();
    }

    public async Task DeleteRecordAsync(int id)
    {
        await DeleteAsync(id);
    }

}