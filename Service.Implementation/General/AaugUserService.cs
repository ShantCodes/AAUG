using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using AutoMapper;

namespace AAUG.Service.Implementations.General;

public class AaugUserService : IAaugUserService
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    public AaugUserService(IAaugUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity)
    {
        var entity = await unitOfWork.AaugUserRepository.AddAsync(mapper.Map<AaugUsersInsertDto>(inputEntity));
        return inputEntity;
    }
}
