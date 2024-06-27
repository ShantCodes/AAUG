
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api.Controllers.General;

[Route("api/AaugUser/")]
public class AaugUserController : ControllerBase
{
    private readonly IAaugUserService userService;
    public AaugUserController(IAaugUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost("InsertAaugUserInfo")]
    public async Task<IActionResult> InsertAaugUserInfo(AaugUserInsertViewModel inputEntity)
    {
        return Ok(await userService.InsertUserInfoAsync(inputEntity));
    }

}
