using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrzyklKol1.Middlewares
{
    public class IndexMiddleware
    {

        private readonly RequestDelegate _next;

        public IndexMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            httpContext.Response.Headers.Add("Index", "s16503");



            await _next(httpContext);

        }

    }
}
