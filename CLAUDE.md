# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository purpose

`BlazorDialog` is a Razor Class Library published as the [`BlazorDialog`](https://www.nuget.org/packages/BlazorDialog) NuGet package. It provides a dialog/modal component that can be used both declaratively (`<Dialog>` in markup) and procedurally as an injected service (`IBlazorDialogService.ShowDialog(...)` / `ShowComponentAsDialog(...)` that you can `await` for a result).

## Toolchain

- .NET SDK pinned in `global.json` — currently `8.0.302`. All projects target `net8.0` via `Directory.Build.props` (which also enables `Nullable`, `ImplicitUsings`, and `WarningsAsErrors=Nullable`).
- The library ships static assets (`wwwroot/blazorDialog.js`, `wwwroot/styles.css`) that must be minified via `gulp` before packing — consumers reference the `.min.*` files.
- Package version is set in `BlazorDialog/BlazorDialog.csproj` (`<Version>`); `VersionSuffix` is appended when passed via build args.

## Common commands

Build library + minify assets + produce NuGet package to `Artifacts/nuget`:

```powershell
./build.ps1                    # release build
./build.ps1 -VersionSuffix rc1 # prerelease suffix appended to package version
```

The script runs `npm install` and `npm run minify` inside `BlazorDialog/`, then `dotnet build BlazorDialog.sln -c Release` and `dotnet pack`. The `-RunTests` switch exists but is a stub — there is no test suite in this repo.

Just minify the JS/CSS (from inside `BlazorDialog/`):

```
npm install
npm run minify      # gulp: clean + uglify JS + cssmin, outputs *.min.js / *.min.css
```

Build & publish the demo app to `artifacts/demoapp`:

```powershell
./build-demo-app.ps1
```

Run a test app during development (from repo root):

```
dotnet run --project TestApps/Server/BlazorDialog.BlazorServerTestApp
dotnet run --project TestApps/Wasm/BlazorDialog.BlazorTestApp.Server
dotnet run --project DemoApp/BlazorDialog.DemoApp/Server
```

## Architecture

### Two ways to use a dialog

Both paths converge on the same `Dialog` component; the difference is who owns its lifecycle.

1. **Declarative** — user places `<Dialog Id="myId">` in their markup. On `OnInitialized`, the component registers itself with `IBlazorDialogStore` under that `Id`. The user then calls `IBlazorDialogService.ShowDialog("myId")` (or uses two-way `@bind-IsShowing`).
2. **Component-as-dialog** — user calls `IBlazorDialogService.ShowComponentAsDialog<TResult>(new ComponentAsDialogOptions(typeof(MyComp)) { Parameters = ... })`. The service registers a `ComponentDialog` in the store, which fires `OnComponentAsDialogsChanged`. `DialogOutput` (which the user must place once in their layout) re-renders and creates a `ComponentAsDialogContainer` that wraps a `Dialog` around a `<DynamicComponent>` for the requested type. The service awaits `RenderTaskCompletionSource` before calling `ShowDialog` on the freshly-rendered dialog.

### Result plumbing

`Dialog.ShowInternal()` creates a `TaskCompletionSource<object?>` and returns its `Task`. `Hide(result)` calls `NotifyDialogHidden` which invokes `OnDialogHide` — an `Action<object?>` that sets the TCS result. This is why the caller can `await` a value returned from arbitrary user code inside the dialog. The child component reads/sets that result via the `DialogContext<TInput>` supplied through the `<DialogInputProvider>` render fragment (input flows in via a cascading `DialogInput` value, output flows back through `ctx.Dialog.Hide(result)`).

### DI registration

`AddBlazorDialog()` in `ServiceCollectionExtensions.cs` registers three scoped services: `IBlazorDialogStore`, `IBlazorDialogService`, `ILocationChangingHandler`. Everything is scoped so state does not leak between circuits/WASM app instances.

### JS interop is narrow

`wwwroot/blazorDialog.js` implements two things: keyboard-close (default `Escape`) and dragging.

For keyboard-close, on `Show` the Dialog passes a `DotNetObjectReference<Dialog>` plus the enabled/key config to `blazorDialog.registerShownDialog`. The JS keeps a stack of open dialogs and only routes the key to the top one via `HideFromKeyPress` (a `[JSInvokable]` on `Dialog`).

For dragging (`AllowDragging`), `OnAfterRenderAsync` hands the content-wrapper `ElementReference` to `blazorDialog.enableDragging` once the dialog is actually in the dom. The JS resolves a drag handle inside that wrapper (an element with `blazor-dialog-drag-handle`, else the `blazor-dialog-header`, skipping elements that belong to a nested dialog), attaches pointer-capture based handlers to it, and moves the wrapper with an inline `transform: translate(...)` clamped to the viewport. There is no interop back to .NET and no document-level listener — the handlers die with the element when the dialog is hidden, which is also what resets the position on the next show.

No other interop is used — z-index, animation, sizing are all pure CSS driven by the `Size`/`Animation`/`BaseZIndex` parameters mapping to classes in `styles.css`.

### Preventing navigation while open

`LocationChangingHandler` wraps `NavigationManager.RegisterLocationChangingHandler` and maintains a stack of open dialogs. It registers a single handler on the first open and disposes it when the stack empties. `PreventNavigation` (default `true`) can be overridden at runtime via `Dialog.ForceAllowNavigation()` / `ForcePreventNavigation()`.

### Depth / z-index for nested dialogs

`Dialog.dialogDepth` is set from `store.GetVisibleDialogsCount()` at show time and added to `BaseZIndex`. This makes dialog-in-dialog stacking work without user intervention.

## Solution & project layout

- `BlazorDialog/` — the actual library. Public API surface is in `IBlazorDialogService`, `IBlazorDialogStore`, `ComponentAsDialogOptions`, `DialogContext<T>`, and the razor components in `Components/`.
- `BlazorDialog.sln` — top-level solution that includes the library + all TestApps (used for manual/interactive verification).
- `TestApps/BlazorDialog.TestAppsCommon/` — shared razor pages (`IndexCommon.razor`, `ComponentsAsDialogsCommon.razor`, `NoServiceCommon.razor`, `ComponentDialog.razor`) that are consumed by both the Server and WASM host apps so behavior can be verified in each render mode without duplicating pages.
- `TestApps/Server/` and `TestApps/Wasm/` — Blazor Server and Blazor WASM (hosted) hosts that reference `TestAppsCommon`.
- `DemoApp/` — has its own `BlazorDialog.DemoApp.sln` and is not part of the main solution. It's the public samples site (deployed to azurewebsites.net) and is built via `build-demo-app.ps1`.
- No automated test project exists. Changes are verified by running the TestApps.

## Consumer-visible setup (from README)

When you're modifying the library, remember consumers wire it up as follows — breaking these contracts is a breaking change:

1. `builder.Services.AddBlazorDialog();`
2. `@using BlazorDialog` in `_Imports.razor`
3. Place `<DialogOutput/>` once in `MainLayout.razor` (or any central interactive spot) — required for `ShowComponentAsDialog` to have somewhere to render.
4. Reference `_content/BlazorDialog/styles.min.css` and `_content/BlazorDialog/blazorDialog.min.js` from `App.razor`.
