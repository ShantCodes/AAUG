﻿using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IAaugUserRepository
{
    Task<AaugUser> AddAsync(AaugUsersInsertDto inputEntity);
    Task<List<AaugUser>> GetUsersAsync();
    IQueryable<AaugUserFullGetDto> GetFullUserInfoByUserId(int Id);
    IQueryable<AaugUser> GetFullUserInfoByUserIdWithTracking(int Id);
    IQueryable<AaugUserGetDto> GetUserByGuId(string guId);
    IQueryable<AaugUserGetDto> GetByUserName(string Username);
}
