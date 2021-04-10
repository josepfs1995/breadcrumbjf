using System;

namespace BreadcrumbJF.Attributes
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class BreadcrumbParentAttribute : Attribute
  {
    public BreadcrumbParentAttribute(string action, string controller, bool showParents = true)
    {
      Action = action;
      Controller = controller;
      ShowParents = showParents;
    }
    public string Action { get; set; }
    public string Controller { get; set; }
    public bool ShowParents { get; set; }
  }
}
