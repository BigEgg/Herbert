namespace Herbert.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ViewModels.UserInfo;
    using Services.UserInfo;
    using System.Net;

    [Route("api/user-info")]
    public class UserInfoController : Controller
    {
        private readonly IApplicationUserService applicationUserService;

        public UserInfoController(IApplicationUserService applicationUserService)
        {
            this.applicationUserService = applicationUserService;
        }

        // GET api/user-info/check-email
        [HttpGet("check-email", Name = "CheckEmail")]
        public IActionResult CheckEmail([FromBody] CheckEmailRequest request)
        {
            if (ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(applicationUserService.IsEmailAlreadyUsed(request.Email));
        }
    }
}
