namespace BreadcrumbJF.ViewModel
{
  public class BreadcrumbViewModel
  {
    public BreadcrumbViewModel(string description, string url)
    {
      Description = description;
      Url = url;
    }
    public string Description { get; set; }
    public string Url { get; set; }
  }
}
