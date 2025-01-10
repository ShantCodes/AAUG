using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.General;

public class ExpandEventFileRepository : EntityRepository<ExpandEventFile>, IExpandEventFileRepository
{
    private readonly IMapper mapper;
    private readonly IAaugUnitOfWork unitOfWork;
    public ExpandEventFileRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
}