using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.General;

public class AaugUserRepository : EntityRepository<AaugUser>, IAaugUserRepository
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    public AaugUserRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public Task<AaugUser> AddAsync(AaugUsersInsertDto inputEntity)
    {
        return AddAsync(mapper.Map<AaugUser>(inputEntity));
    }
}
