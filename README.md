# Blazor Dialog

[![Build status](https://dev.azure.com/stavros-kasidis/Blazor%20Dialog/_apis/build/status/Blazor%20Dialog-CI)](https://dev.azure.com/stavros-kasidis/Blazor%20Dialog/_build/latest?definitionId=16) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/BlazorDialog.svg?logo=nuget)](https://www.nuget.org/packages/BlazorDialog) [![Nuget](https://img.shields.io/nuget/dt/BlazorDialog.svg?logo=nuget)](https://www.nuget.org/packages/BlazorDialog) [![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=7CRGWPYB5AKJQ&currency_code=EUR&source=url)

Dialog component as a service for [Blazor](https://blazor.net)!

TODO: Add gif

> ⚠️
> WORK IN PROGRESS: Do not use in production yet

## Features
* Call a dialog as a service and `await` for the result !
* Can use dialogs as normal components (if you don't want to use as a service)
* Build-in modal dialog (similar to bootstrap)
* Option to use completely custom markup/css (without using the build-in opinionated css and html)


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

<details open="open"><summary>0.3</summary>
    
>- Upgrated to 3.1
>- Added new helper components: `DialogHeader`, `DialogBody`, `DialogFooter`
</details>

<details><summary>0.1</summary>
    
>- Initial release.
</details>
