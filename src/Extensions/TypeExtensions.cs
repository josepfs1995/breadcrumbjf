using System;
using System.Reflection;

namespace BreadcrumbJF.Extensions
{
  internal static class TypeExtensions
  {
    internal static MethodInfo GetMethod(this Type? type, string method)
    {
      return type?.GetMethod(method);
    }
  }
}
