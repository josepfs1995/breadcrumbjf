# BreadcrumbJF
![License](https://img.shields.io/github/license/josepfs1995/breadcrumbjf)

## Introduction

 This project is a generator of breadcrumb for your Web NET Core.

 Esto es un generador de Breadcrumb para tu pagina web en NET CORE

## Attributes
**BreadcrumbAttribute**<br>
This class receive just a parameter with the name Description (one per method)


**BreadcrumbParentAttribute**<br>
This class migth work for indicate your parent breadcrumb (n per method), receive 3 parameters (Action, Controller and ShowParent(Default true))


## Installation

First step: You need write this code in the section Configure in your startup:

Necesitas escribir este codigo en la sección Configure de tu startup:
```
services.AddBreadcrumbJF(GetType().Assembly);
```
Second step: You need add the attributes in your methods
```
[Breadcrumb("Index")]
public IActionResult Index(string returnUrl)
{
  return View();
}
```
Or
```
[BreadcrumbParent("Privacy", "Home")]
[Breadcrumb("Hijo")]
public IActionResult Index()
{
  return View();
}
[Breadcrumb("Papá")]
public IActionResult Privacy()
{
  return View();
}
```
Third step: You need create your Views in **/Views/Shared/Components/Breadcrumb/Default.cshtml**.
This view must receive a model of type **@model IEnumerable<BreadcrumbJF.ViewModel.BreadcrumbViewModel>**

Example:
```
@model IEnumerable<BreadcrumbJF.ViewModel.BreadcrumbViewModel>

<nav aria-label="breadcrumb">
    <ol class="breadcrumb">
        @foreach (var item in Model.Select((Value, Index) => new {Value = Value, Index=Index }))
        {
            <li class="breadcrumb-item @(item.Index == (Model.Count() - 1) ? "active": "")">
                <a href="@item.Value.Url">
                    @item.Value.Description
                </a>
            </li>
        }
    </ol>
</nav>
```
Fouth step: You need to add reference in your _ViewImports.cshtml (/Views/_ViewImports.cshtml)
```
@addTagHelper *, BreadcrumbJF
```
Fifth step: You need to make use of ViewComponent in your Layout.cshtml
```
@{
    ViewData["Title"] = "Home Page";
}
<vc:breadcrumb></vc:breadcrumb>
<div class="text-center">
    <h1 class="display-4">Welcome</h1>
    <p>Learn about <a href="https://docs.microsoft.com/aspnet/core">building Web apps with ASP.NET Core</a>.</p>
</div>

```
