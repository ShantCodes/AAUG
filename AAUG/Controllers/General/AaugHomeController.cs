using AAUG.Service.Interfaces.General;
using AAUG.Service.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Controllers.General
{
    [Route("api/")]

    public class AaugHomeController : ControllerBase
    {
        private readonly IAaugTest aaugTest;
        private readonly INewsService newsService;
        public AaugHomeController(IAaugTest aaugTest, INewsService newsService)
        {
            this.aaugTest = aaugTest;
            this.newsService = newsService;
        }

        [HttpGet("TestArchitecture/{test}")]
        public async Task<IActionResult> Test(string test)
        {
            return Ok(aaugTest.Hello(test));
        }

        [HttpGet("GetNewsData")]
        public async Task<IActionResult> GetNewsData()
        {
            return Ok(await newsService.GetNewsAsync());
        }

        
    }
}
