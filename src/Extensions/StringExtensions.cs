using System;
using System.Reflection;

namespace BreadcrumbJF.Extensions
{
  internal static class StringExtensions
  {
    internal static Type GetController(this string value, Assembly assembly, string area)
    {
      area = area != null ? $".Areas.{area}" : "";
      var name = $"{assembly.FullName.Split(',')[0]}{area}.Controllers.{value.RemoveControllerText()}Controller";
      var controllerType = assembly.GetType(name);
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
