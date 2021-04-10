using System.Reflection;

namespace BreadcrumbJF.Extensions
{
  internal interface IBreadcrumbInitialJF
  {
    public Assembly Assembly { get; }
  }
  internal class BreadcrumbInitialJF : IBreadcrumbInitialJF
  {
    public BreadcrumbInitialJF(Assembly assembly)
    {
      Assembly = assembly;
    }
    public Assembly Assembly { get; }
  }
}
