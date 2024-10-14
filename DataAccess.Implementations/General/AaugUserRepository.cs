using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

    public IQueryable<AaugUserGetDto> GetNotApprovedAaugUsers()
    {
        return mapper.ProjectTo<AaugUserGetDto>(
            GetData(a => !a.IsApproved)
        );
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
                ProfilePictureFileId = u.ProfilePictureFileId,
                Subscribed = u.Subscribed,
                IsSubApproved = u.IsSubApproved,
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

    public IQueryable<AaugUserGetDto> GetIsSubApprovedUsers()
    {
        return mapper.ProjectTo<AaugUserGetDto>(GetData(a => a.IsSubApproved));
    }

    public IQueryable<AaugUserGetDto> GetUsers()
    {
        var x = GetData();
        var data = mapper.ProjectTo<AaugUserGetDto>(x
            );

        return data;
    }

    public IQueryable<AaugUserGetDto> GetApprovedAaugUsers()
    {
        return mapper.ProjectTo<AaugUserGetDto>(
            GetData(a => a.IsApproved)
        );
    }

    public IQueryable<AaugUserGetDto> GetSubscribedNotSubApprovedUsers()
    {
        return mapper.ProjectTo<AaugUserGetDto>(GetData(a => a.Subscribed && !a.IsSubApproved));
    }

    public IQueryable<AaugUserWithProfilePictureGetDto> SearchAaugUser(string name)
    {
        return mapper.ProjectTo<AaugUserWithProfilePictureGetDto>(
            GetData(a => a.Name.Contains(name)
            || a.NameArmenian.Contains(name)
            || a.LastName.Contains(name)
            || a.LastNameArmenian.Contains(name)
            || a.Email.Contains(name))
        );
    }

    public IQueryable<AaugUserFullGetDto> GetFullUserInfoByUserId(int Id)
    {
        return mapper.ProjectTo<AaugUserFullGetDto>(
            GetData(a => a.Id == Id)
        );
    }
    public IQueryable<AaugUserFullGetDto> GetFullUserInfoByPhone(string phone)
    {
        return mapper.ProjectTo<AaugUserFullGetDto>(
            GetData(a => a.Phone == phone)
        );
    }

    public IQueryable<AaugUser> GetAaugUserById(int id)
    {
        return GetData(a => a.Id == id);
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
    public IQueryable<AaugUserGetDto> GetByUserName(string Username)
    {
        return mapper.ProjectTo<AaugUserGetDto>(
            GetData(a => a.Email == Username)
        );
    }

    public Task<AaugUser> DeleteUserAsync(int aaugUserId)
    {
        return DeleteAsync(aaugUserId);
    }

}
