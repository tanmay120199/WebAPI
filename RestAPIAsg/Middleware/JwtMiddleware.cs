using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestAPIAsg.Models;
using RestAPIAsg.Services;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RestAPIAsg.Middleware
{
   
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IAuthenticateService jwtUtils)
        {
            try
            {
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                var userId = jwtUtils.ValidateToken(token);
                if (userId != null)
                {
                    // attach user to context on successful jwt validation
                    //context.Items["User"] = userService.GetById(userId);
                }

                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleGlobalExceptionAsync(context, ex).ConfigureAwait(false);
            }
            
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(new GlobalErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Something went wrong !Internal Server Error"
            }.ToString());
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }

    public static class GlobalExceptionMiddleware
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
