namespace AAUG.DomainModels.Dtos;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    //User info
    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? NameArmenian { get; set; }

    public string? LastNameArmenian { get; set; }

    public string? Email { get; set; }
}

// public class ResetPasswordDto
// {
//     public string Email { get; set; }
//     public string Token { get; set; }
//     public string Password { get; set; }
//     public string ConfirmPassword { get; set; }
// }
