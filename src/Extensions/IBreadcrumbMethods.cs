using BreadcrumbJF.ViewModel;
using System.Reflection;

namespace BreadcrumbJF.Extensions
{
  public interface IBreadcrumbMethods
  {
    MethodInfo GetMethodInfoByControllerAndAction(string controller, string action, string area = null);
    bool ContainsParent(MethodInfo methodInfo);
    BreadcrumbViewModel GetBreadcrumbDescription(MethodInfo methodInfo, string url);
  }
}
