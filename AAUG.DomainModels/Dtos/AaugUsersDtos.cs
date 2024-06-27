namespace AAUG.DomainModels.Dtos;

public class AaugUsersInsertDto
{
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public int? RoleId { get; set; }

    public short MajorsId { get; set; }

    public short? TalentsId { get; set; }

    public int? ProfilePictureFileId { get; set; }

    public int NationalCardFileId { get; set; }

    public int UniversityCardFileId { get; set; }

    public string? Email { get; set; }

    public bool? CanGetNotfiedByMail { get; set; }

    public bool IsApproved { get; set; }
}
