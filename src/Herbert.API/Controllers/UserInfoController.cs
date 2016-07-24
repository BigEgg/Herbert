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

        // POST api/user-info/register
        [HttpPost("register", Name = "Register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if (ModelState.IsValid) { return BadRequest(ModelState); }

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
