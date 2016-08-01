namespace Herbert.API.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    using Herbert.API.Controllers.Filters;
    using Herbert.API.ViewModels.UserInfo;
    using Herbert.Services.UserInfo;

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
        [ValidationRequest]
        public IActionResult CheckEmail([FromBody] CheckEmailRequest request)
        {
            return Ok(applicationUserService.IsEmailAlreadyUsed(request.Email));
        }

        // POST api/user-info/register
        [HttpPost("register", Name = "Register")]
        [ValidationRequest]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (applicationUserService.IsEmailAlreadyUsed(request.Email)) { return StatusCode((int)HttpStatusCode.Conflict); }

            var user = applicationUserService.NewUser(request.Email, request.Password, request.NickName, request.RegisterSourceType);

            return CreatedAtRoute("SignIn", new { });
        }

        // POST api/user-info/sign-up
        [HttpPost("sign-up", Name = "SignUp")]
        public IActionResult SignUp([FromBody] SignUpRequest request)
        {
            return NotFound();
        }
    }
}
