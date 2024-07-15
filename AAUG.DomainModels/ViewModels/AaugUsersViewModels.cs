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
