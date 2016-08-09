namespace Herbert.API.Controllers.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// The Attribute class for validating request body
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.ActionFilterAttribute" />
    public class ValidateRequestAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Check request body is valid or not.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
            }
        }
    }
}
