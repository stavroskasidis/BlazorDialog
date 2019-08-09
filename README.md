# Blazor Dialog

# Blazor Context Menu

TODO: Add build status/nuget shields

Dialog component as a service for [Blazor](https://blazor.net)!

TODO: Add gif

> ⚠️ Warning

> This project is build on top of an experimental framework. There are many limitations and there is a high propability that there will be breaking changes each version.

## Samples / Demo
TODO: Add Demo

## Installation
**1. Add the nuget package in your Blazor project**
```
> dotnet add package BlazorDialog

OR

PM> Install-Package BlazorDialog
```
*Nuget package page can be found [here](https://www.nuget.org/packages/BlazorDialog).*

**2. Add the following line in your Blazor project's startup class**

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddBlazorDialog();
    }
}
```
**3. Add the following line in your `_Imports.razor`**
```csharp
@using BlazorDialog
```
**4. Reference the css file**

Add the following static file reference in your `_Host.cshtml` (server-side blazor) or in your `index.html` (client-side blazor). 
Make sure that there is a call to `app.UseStaticFiles();` in your server project's `Startup.cs`.

```html
<link href="_content/BlazorDialog/styles.min.css" rel="stylesheet" />
```

## Basic usage

```xml
TODO: write
```


## ⚠️ Breaking changes ⚠️

None so far

## Release Notes

TODO: Write
