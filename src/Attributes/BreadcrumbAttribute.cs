using System;
namespace BreadcrumbJF.Attributes
{
  public class BreadcrumbAttribute:Attribute
  {
    public BreadcrumbAttribute(string description)
    {
      Description = description;
    }
    public string Description { get; set; }
  }
}
