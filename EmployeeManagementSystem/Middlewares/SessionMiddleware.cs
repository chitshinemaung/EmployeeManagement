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
            var path = context.Request.Path.ToString().ToLower();
            if (path.StartsWith("/login"))
            {
                await _next(context);
                return;
            }

            var loginData = context.Session.GetString("Login");
            if (string.IsNullOrEmpty(loginData))
            {
                context.Response.Redirect("/Login/UserLoginIndex");
                return;
            }

            //if (context.Request.Path.ToString().ToLower() != "/login")
            //{
            //    var loginData = context.Session.GetString("Login");

            //    if (string.IsNullOrEmpty(loginData))
            //    {

            //        context.Response.Redirect("/Login/UserLoginIndex");
            //    }

            //}


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
