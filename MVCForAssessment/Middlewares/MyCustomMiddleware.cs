using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCForAssessment.Middlewares
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public MyCustomMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            //await context.Response.WriteAsync("My Custom middleware");
            //return _requestDelegate(context);
           await _requestDelegate(context);
        }
    }
    public static class MyCustomeMiddlewareExtension
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
    
}
