﻿namespace AAUG.DomainModels.Dtos;

public class AaugUsersInsertDto
{
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string UserId { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public int? RoleId { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int? NationalCardFileId { get; set; }

    public int? UniversityCardFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool IsApproved { get; set; }
}

public class AaugUsersEditDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string UserId { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public int? RoleId { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int? NationalCardFileId { get; set; }

    public int? UniversityCardFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool IsApproved { get; set; }
}

public class AaugUserGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string UserId { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }
}

public class AaugUserFullGetDto
{
    public int? Id { get; set; }

    public string? UserId { get; set; }

    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int? NationalCardFileId { get; set; }

    public int? UniversityCardFileId { get; set; }
    public int? ReceiptFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool? IsApproved { get; set; }
    public DateTime? SubscribeDate { get; set; }
}

public class SubscribeFileDto
{
    public int Id { get; set; }
    public int? ReceiptFileId { get; set; }
    public DateOnly SubscribeDate { get; set; }
}



