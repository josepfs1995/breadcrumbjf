using BreadcrumbJF.Attributes;
using BreadcrumbJF.Extensions;
using BreadcrumbJF.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BreadcrumbJF.Components
{
  public class BreadcrumbViewComponent : ViewComponent
  {
    public readonly IBreadcrumbMethods _breadcrumbMethods;
    public BreadcrumbViewComponent(IBreadcrumbMethods breadcrumbMethods)
    {
      _breadcrumbMethods = breadcrumbMethods;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      var propertyInfo = HttpContext.Request.GetType().GetProperty("RouteValues");
      var routeValues = propertyInfo.GetValue(HttpContext.Request) as RouteValueDictionary;

      var area = routeValues["area"]?.ToString();
      var controller = routeValues["controller"].ToString();
      var action = routeValues["action"].ToString();

      var methodInfo = _breadcrumbMethods.GetMethodInfoByControllerAndAction(controller, action, area);

      List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
      GetBreadcrumbDescriptionByParent(methodInfo, GetUrl(action, controller, area), breadcrumbs);
      return View(breadcrumbs);
    }
    private string GetUrl(string action,string controller, string area = null)
    {
      if(area == null)
      {
        return Url.Action(action, controller, new { Area = area });
      }
      return Url.Action(action, controller);
    }
    private void GetBreadcrumbDescriptionByParent(MethodInfo methodInfo, string url, List<BreadcrumbViewModel> breadcrumbs)
    {
      if (!_breadcrumbMethods.ContainsParent(methodInfo))
      {
        breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(methodInfo, url));
        return;
      }

      var breadcrumbParents = methodInfo.GetCustomAttributes(typeof(BreadcrumbParentAttribute));
      MethodInfo methodResponse = null;
      string urlResponse = "/";
      foreach(var item in breadcrumbParents)
      {
        var breadcrumb = item as BreadcrumbParentAttribute;
        methodResponse = _breadcrumbMethods.GetMethodInfoByControllerAndAction(breadcrumb.Controller, breadcrumb.Action, breadcrumb.Area);
        urlResponse = GetUrl(breadcrumb.Action, breadcrumb.Controller, breadcrumb.Area);
        if (breadcrumb.ShowParents)
          GetBreadcrumbDescriptionByParent(methodResponse, urlResponse, breadcrumbs);
        else
          breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(methodResponse, urlResponse));
      }
      breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(methodInfo, url));
    }
  }
}
