using System;
using System.Reflection;

namespace BreadcrumbJF.Extensions
{
  internal static class StringExtensions
  {
    internal static Type GetController(this string value, Assembly assembly)
    {
      var controllerType = assembly.GetType($"Breadcrumb.Controllers.{value.RemoveControllerText()}Controller");
      return controllerType;
    }
    internal static string RemoveControllerText(this string value)
    {
      if (!ContainsController(value)) return value;

      return value.Replace("Controller", "");
    }
    internal static bool ContainsController(string controller)
    {
      return controller.ToLower().Contains("controller");
    }
  }
}
