namespace Herbert.API.Controllers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    using Herbert.API.Controllers.Filters;
    using Herbert.API.ViewModels.UserInfo;
    using Herbert.Services.UserInfo;

    [TypeFilter(typeof(ApplicationAuthorizeAttribute))]
    [ValidateRequest]
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
            return Ok(new CheckEmailResponse(applicationUserService.IsEmailAlreadyUsed(request.Email)));
        }

        // POST api/user-info/register
        [HttpPost("signup", Name = "SignUp")]
        public IActionResult SignUp([FromBody] SignUpRequest request)
        {
            if (applicationUserService.IsEmailAlreadyUsed(request.Email)) { return StatusCode((int)HttpStatusCode.Conflict); }

            var user = applicationUserService.NewUser(request.Email, request.Password, request.NickName, request.RegisterSourceType);

            return CreatedAtRoute("LogIn", new { });
        }

        // POST api/user-info/sign-up
        [HttpPost("login", Name = "LogIn")]
        public IActionResult LogIn([FromBody] LogInRequest request)
        {
            return NotFound();
        }
    }
}
