using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.Service.Interfaces.General;
using AutoMapper;

namespace AAUG.Service.Implementations.General;

public class EventDetails : IEventDetails
{
    private IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public EventDetails(IAaugUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    
}