using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using BreadcrumbJF.Extensions;

namespace BreadcrumbJF
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
