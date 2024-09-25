using AAUG.DomainModels.Dtos.Media;
using Microsoft.AspNetCore.Http;

namespace AAUG.DomainModels.ViewModels;

public class AaugUserInsertViewModel
{
    public string Name { get; set; } = null!;
    public string UserId { get; set; }

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }

}

public class AaugUserFullInsertViewModel
{

    public IFormFile? ProfilePictureFile { get; set; }

    public IFormFile? NationalCardFile { get; set; }

    public IFormFile? UniversityCardFile { get; set; }
    public IFormFile? ReceiptFile { get; set; }

}

public class AaugUserFullEditViewModel
{
    public string? Name { get; set; }

    public string? LastName { get; set; }

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public short? MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public IFormFile? ProfilePictureFile { get; set; }

    public IFormFile? NationalCardFile { get; set; }

    public IFormFile? UniversityCardFile { get; set; }
    public IFormFile? ReceiptFile { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }


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

    public int? ProfilePictureFileId { get; set; }
    public MediaFileGetDto? ProfilePictureFile { get; set; }

    public int? NationalCardFileId { get; set; }
    public MediaFileGetDto? NationalCardFile { get; set; }

    public int? UniversityCardFileId { get; set; }
    public MediaFileGetDto? UniversityCardFile { get; set; }

    public int? ReceiptFileId { get; set; }
    public MediaFileGetDto? ReceiptFile { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }


    public bool? CanGetNotfiedByMail { get; set; }

    public DateTime? SubscribeDate { get; set; }
    public bool IsApproved { get; set; }
}

public class AaugUserWithProfilePicureGetViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? UserId { get; set; }

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }

    public int? ProfilePictureFileId { get; set; }
    public MediaFileGetDto? ProfilePictureFile { get; set; }
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

    public int? ProfilePictureFileId { get; set; }
    public IList<string>? Role { get; set; }

    public bool Subscribed { get; set; }
    public bool IsSubApproved { get; set; }
}

