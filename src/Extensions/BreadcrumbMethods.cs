using BreadcrumbJF.Attributes;
using BreadcrumbJF.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace BreadcrumbJF.Extensions
{
  internal sealed class BreadcrumbMethods: IBreadcrumbMethods
  {
    public readonly IBreadcrumbInitialJF _breadcrumbJF;
    public BreadcrumbMethods(IBreadcrumbInitialJF breadcrumbJF)
    {
      _breadcrumbJF = breadcrumbJF;
    }
    public MethodInfo GetMethodInfoByControllerAndAction(string controller, string action)
    {
      var controllerType = controller.GetController(_breadcrumbJF.Assembly);
      return controllerType.GetMethods().FirstOrDefault(x=> !x.GetCustomAttributes(typeof(HttpPostAttribute)).Any() && !x.GetCustomAttributes(typeof(HttpPutAttribute)).Any() && !x.GetCustomAttributes(typeof(HttpDeleteAttribute)).Any() && x.Name == action);
    }
    public bool ContainsParent(MethodInfo methodInfo)
    {
      return methodInfo.GetCustomAttributes(typeof(BreadcrumbParentAttribute))?.Count() > 0;
    }
    public BreadcrumbViewModel GetBreadcrumbDescription(MethodInfo methodInfo, string url)
    {
      var attribute = methodInfo.GetCustomAttribute(typeof(BreadcrumbAttribute));
      if (attribute == null) return null;

      var breadcrumb = attribute as BreadcrumbAttribute;
      return new BreadcrumbViewModel(breadcrumb.Description, url);
    }
  
  }
}
