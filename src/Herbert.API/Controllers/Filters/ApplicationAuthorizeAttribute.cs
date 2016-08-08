namespace Herbert.API.Controllers.Filters
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Primitives;
    using System;
    using System.Linq;

    using Herbert.API.Helpers;
    using Herbert.API.ViewModels.Access;
    using Herbert.Models.Access;
    using Herbert.Services.Access;

    /// <summary>
    /// The Attribute class for check is valid Application to call this API
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ApplicationAuthorizeAttribute : ActionFilterAttribute
    {
        private const string APP_AUTHORIZE_SOURCE_TYPE_KEY = "X-Herbert-ClientSource";
        private const string APP_AUTHORIZE_TOKEN_KEY = "X-Herbert-Authenticate";
        private readonly ISupportApplicationService supportApplicationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationAuthorize"/> class.
        /// </summary>
        /// <param name="supportApplicationService">The support application service.</param>
        public ApplicationAuthorizeAttribute(ISupportApplicationService supportApplicationService)
        {
            this.supportApplicationService = supportApplicationService;
        }


        /// <summary>
        /// Check request is from authorized application or not.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var typeRaw = GetHeaderValue(actionContext.HttpContext.Request, APP_AUTHORIZE_SOURCE_TYPE_KEY);
            var tokenRaw = GetHeaderValue(actionContext.HttpContext.Request, APP_AUTHORIZE_TOKEN_KEY);
            if (string.IsNullOrEmpty(typeRaw) || string.IsNullOrWhiteSpace(tokenRaw))
            {
                actionContext.Result = new UnauthorizedResult();
                return;
            }

            SupportApplicationType type;
            if (!Enum.TryParse(typeRaw, out type))
            {
                actionContext.Result = new UnauthorizedResult();
                return;
            }

            var token = tokenRaw.Base64Decode().Deserialize<ApplicationTokenVM>();
            if (token == null)
            {
                actionContext.Result = new UnauthorizedResult();
                return;
            }

            var applicationType = supportApplicationService.GetApplicationType(token.AppId, token.AppSecret);
            if (!applicationType.HasValue || applicationType.Value != type)
            {
                actionContext.Result = new UnauthorizedResult();
                return;
            }
        }

        private string GetHeaderValue(HttpRequest request, string key)
        {
            var values = request.Headers.GetCommaSeparatedValues(key);

            if (StringValues.IsNullOrEmpty(values))
            {
                return string.Empty;
            }
            else
            {
                return values.First();
            }
        }
    }
}
