namespace EmployeeManagementSystem.Middlewares
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;
        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.ToString().ToLower() != "/Login/UserLogin")
            {
                await _next(context);
                return;
            }
            var loginData = context.Session.GetString("Login");

            if (string.IsNullOrEmpty(loginData))
            {

                context.Response.Redirect("/Login/UserLogin");
            }

            await _next(context);
        }
    }

    public static class SessionMiddlewareExtensions
    {
        public static IApplicationBuilder UseSessionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SessionMiddleware>();
        }
    }
}
