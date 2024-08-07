﻿using Microsoft.AspNetCore.Http;

namespace AAUG.DomainModels.ViewModels;

public class AaugUserInsertViewModel
{
    public string Name { get; set; } = null!;
    public string UserId { get; set; }

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public string? Email { get; set; }
}

public class AaugUserFullInsertViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public IFormFile? ProfilePictureFile { get; set; }

    public IFormFile? NationalCardFile { get; set; }

    public IFormFile? UniversityCardFile { get; set; }
    public IFormFile? ReceiptFile {get; set;}

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }
}

public class AaugUserFullEditViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public IFormFile? ProfilePictureFile { get; set; }

    public IFormFile? NationalCardFile { get; set; }

    public IFormFile? UniversityCardFile { get; set; }
    public IFormFile? ReceiptFile {get; set;}

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }
}
public class AaugUserFullGetViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public IFormFile? ProfilePictureFile { get; set; }

    public IFormFile? NationalCardFile { get; set; }

    public IFormFile UniversityCardFile { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }
}

public class AaugUserGetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? UserId { get; set; }

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public string? Email { get; set; }

}
