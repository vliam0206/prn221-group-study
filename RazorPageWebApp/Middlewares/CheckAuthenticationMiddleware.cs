namespace RazorPageWebApp.Middlewares;

public class CheckAuthenticationMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var currentUser = context.Session.GetString(AppConstants.USER_NAME);
        var path = context.Request.Path;
        if (path.HasValue && PathCondition(path) 
                    && !PathConditionNotInclude(path))
        {
            if (string.IsNullOrEmpty(currentUser))
            {
                context.Response.Redirect("/Auth/Login");
                return;
            }
        }
        await next(context);
    }
    private bool PathCondition(PathString path)
    {
        return (
            path.Value.ToLower().StartsWith("/UserScreen".ToLower())
            );
    }
    private bool PathConditionNotInclude(PathString path)
    {
        return (
            path.Value.ToLower().StartsWith("/Index".ToLower())
         && path.Value.ToLower().StartsWith("/Community".ToLower())
         && path.Value.ToLower().StartsWith("/Auth".ToLower())
            );
    }
}
