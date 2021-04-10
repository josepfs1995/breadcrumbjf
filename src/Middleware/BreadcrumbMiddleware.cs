using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BreadcrumbJF.Middleware
{
  public class BreadcrumbMiddleware
  {
    private readonly RequestDelegate _next;
    public BreadcrumbMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      await _next(context);
    }
  }
}
