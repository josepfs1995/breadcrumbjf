using System;

namespace BreadcrumbJF.Attributes
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class BreadcrumbParentAttribute : Attribute
  {
    public BreadcrumbParentAttribute(string action, string controller, string area = null, bool showParents = true)
    {
      Action = action;
      Controller = controller;
      Area = area;
      ShowParents = showParents;
    }
    public string Action { get; set; }
    public string Controller { get; set; }
    public string Area { get; set; }
    public bool ShowParents { get; set; }
  }
}
