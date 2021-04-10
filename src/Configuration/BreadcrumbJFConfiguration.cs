using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using BreadcrumbJF.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.FileProviders;
using BreadcrumbJF.Components;
using System.Collections.Generic;

namespace BreadcrumbJF.Configuration
{
  public static class BreadcrumbJFConfiguration
  {
    public static IServiceCollection AddBreadcrumb(this IServiceCollection services, Assembly assembly)
    {
      if (assembly == null) throw new ArgumentNullException(nameof(assembly));

      services.AddScoped<IBreadcrumbInitialJF>(x =>
      {
        return new BreadcrumbInitialJF(assembly);
      });
      services.AddScoped<IBreadcrumbMethods, BreadcrumbMethods>();

      

      return services;
    }
}
}
