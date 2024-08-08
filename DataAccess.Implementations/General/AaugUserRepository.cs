using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

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

    public Task<List<AaugUser>> GetUsersAsync()
    {
        var data = GetData()
            .Select(u => new AaugUser
            {
                Id = u.Id,
                UserId = u.UserId,
                Name = u.Name,
                LastName = u.LastName,
                NameArmenian = u.NameArmenian,
                LastNameArmenian = u.LastNameArmenian,
                // MajorsId = u.MajorsId ?? default(short?),
                // TalentsId = u.TalentsId ?? default(short?),
                // ProfilePictureFileId = u.ProfilePictureFileId ?? default(int?),
                // NationalCardFileId = u.NationalCardFileId ?? default(int?),
                // UniversityCardFileId = u.UniversityCardFileId ?? default(int?),
                // Email = u.Email,
                // CanGetNotfiedByMail = u.CanGetNotfiedByMail ?? default(bool?),
                // IsApproved = u.IsApproved ?? default(bool?)
            })
            .ToListAsync();

        return data;
    }

    public IQueryable<AaugUserFullGetDto> GetFullUserInfoByUserId(int Id)
    {
        return mapper.ProjectTo<AaugUserFullGetDto>(
            GetData(a => a.Id == Id)
        );
    }

    public IQueryable<AaugUser> GetFullUserInfoByUserIdWithTracking(int Id)
    {
        return GetData(a => a.Id == Id);        
    }

    public IQueryable<AaugUserGetDto> GetUserByGuId(string guId)
    {
        return mapper.ProjectTo<AaugUserGetDto>(
            GetData(a => a.UserId == guId)
        );
    }

}
