using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Controllers.General
{
    [Route("api/")]

    public class AaugHomeController : ControllerBase
    {
        private readonly IAaugTest aaugTest;
        public AaugHomeController(IAaugTest aaugTest)
        {
            this.aaugTest = aaugTest;
        }

        [HttpGet("TestArchitecture/{test}")]
        public async Task<IActionResult> Test(string test)
        {
            return Ok(aaugTest.Hello(test));
        }
    }
}
