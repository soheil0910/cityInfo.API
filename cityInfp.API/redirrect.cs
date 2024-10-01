namespace cityInfp.API
{
    public class redirrect
    {



        private readonly RequestDelegate _next;
        private readonly ILogger<redirrect> _logger;

        public redirrect(RequestDelegate next, ILogger<redirrect> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 404)
            {
                context.Response.StatusCode = 404;
                //context.Response.ContentType = "application/json";
                context.Response.ContentType = "text/html";

                //await context.Response.WriteAsync("<h1>nnnnnnn_NotFond</h1>");
                context.Response.Redirect("/swagger/index.html");
            }
        }


    }
}
