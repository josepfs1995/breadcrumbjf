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

      var controller = routeValues["controller"].ToString();
      var action = routeValues["action"].ToString();

      var methodInfo = _breadcrumbMethods.GetMethodInfoByControllerAndAction(controller, action);

      List<BreadcrumbViewModel> breadcrumbs = new List<BreadcrumbViewModel>();
      GetBreadcrumbDescriptionByParent(methodInfo, Url.Action(action, controller), breadcrumbs);
      return View(breadcrumbs);
    }

    private void GetBreadcrumbDescriptionByParent(MethodInfo methodInfo, string url, List<BreadcrumbViewModel> breadcrumbs)
    {
      if (!_breadcrumbMethods.ContainsParent(methodInfo))
      {
        breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(methodInfo, url));
        return;
      }

      var breadcrumbParents = methodInfo.GetCustomAttributes(typeof(BreadcrumbParentAttribute));
      foreach(var item in breadcrumbParents)
      {
        var breadcrumb = item as BreadcrumbParentAttribute;
        if(breadcrumb.ShowParents)
          GetBreadcrumbDescriptionByParent(_breadcrumbMethods.GetMethodInfoByControllerAndAction(breadcrumb.Controller, breadcrumb.Action), Url.Action(breadcrumb.Action, breadcrumb.Controller), breadcrumbs);
        else
          breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(_breadcrumbMethods.GetMethodInfoByControllerAndAction(breadcrumb.Controller, breadcrumb.Action), Url.Action(breadcrumb.Action, breadcrumb.Controller)));
      }
      breadcrumbs.Add(_breadcrumbMethods.GetBreadcrumbDescription(methodInfo, url));
    }
  }
}
