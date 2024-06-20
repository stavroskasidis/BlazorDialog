using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class ComponentAsDialogOptions
    {
        public ComponentAsDialogOptions(Type componentType)
        {
            ComponentType = componentType;
        }

        /// <summary>
        /// The type of the rendered component
        /// </summary>
        public Type ComponentType { get; protected set; }

        /// <summary>
        /// The parameters of the rendered component.
        /// </summary>
        public Dictionary<string, object> Parameters { get; set; } = new();

        /// <summary>
        /// If set to true then all default html/css is removed. Use when custom dialog implementations are needed.
        /// </summary>
        public bool IsCustom { get; set; }

        /// <summary>
        /// Allows you to change the appearing animation. Ignored when the dialog is <see cref="Dialog.IsCustom" />.
        /// </summary>
        public DialogAnimation Animation { get; set; } = DialogAnimation.None;

        /// <summary>
        /// The base z-index of the dialog when shown. Default: 1050
        /// </summary>
        public int BaseZIndex { get; set; } = 1050;

        /// <summary>
        /// Allows you to set the positioning of the dialog from the top. Ignored when the dialog is <see cref="Dialog.IsCustom" />.
        /// </summary>
        public bool Centered { get; set; }

        /// <summary>
        /// Adds a custom css class to the wrapper of the dialog.
        /// </summary>
        public string? CssClass { get; set; }

        /// <summary>
        /// Allows you to set the dialog size. Ignored when the dialog is <see cref="Dialog.IsCustom" />.
        /// </summary>
        public DialogSize Size { get; set; } = DialogSize.Normal;

        /// <summary>
        /// Allows you to prevent browser navigation when the dialog is shown. Defaults to true.
        /// </summary>
        public bool PreventNavigation { get; set; } = true;

        /// <summary>
        /// An event that is triggered before the dialog appears.
        /// </summary>
        public Func<DialogBeforeShowEventArgs, Task>? OnBeforeShow { get; set; }

        /// <summary>
        /// An event that is triggered after the dialog appears.
        /// </summary>
        public Func<DialogAfterShowEventArgs, Task>? OnAfterShow { get; set; }

        /// <summary>
        /// An event that is triggered before the dialog hides.
        /// </summary>
        public Func<DialogBeforeHideEventArgs, Task>? OnBeforeHide { get; protected set; }

        /// <summary>
        /// An event that is triggered after the dialog hides.
        /// </summary>
        public Func<DialogAfterHideEventArgs, Task>? OnAfterHide { get; protected set; }

        public Func<bool, Task>? OnAfterRender { get; protected set; }

        internal event Func<Task> OnOptionsChanged;

        ///// <summary>
        ///// An event that is triggered before the dialog appears.
        ///// </summary>
        //public async Task SetOnBeforeShow(Func<DialogBeforeShowEventArgs, Task> func)
        //{
        //    OnBeforeShow = func;
        //    await RefreshDialog();
        //}

        ///// <summary>
        ///// An event that is triggered after the dialog appears.
        ///// </summary>
        //public async Task SetOnAfterShow(Func<DialogAfterShowEventArgs, Task> func)
        //{
        //    OnAfterShow = func;
        //    await RefreshDialog();
        //}

        /// <summary>
        /// An event that is triggered before the dialog hides.
        /// </summary>
        public async Task SetOnBeforeHide(Func<DialogBeforeHideEventArgs, Task> func)
        {
            OnBeforeHide = func;
            await RefreshDialog();
        }

        /// <summary>
        /// An event that is triggered after the dialog hides.
        /// </summary>
        public async Task SetOnAfterHide(Func<DialogAfterHideEventArgs, Task> func)
        {
            OnAfterHide = func;
            await RefreshDialog();
        }

        /// <summary>
        /// An event that is triggered after the dialog hides.
        /// </summary>
        public async Task SetOnAfterRender(Func<bool, Task> func)
        {
            OnAfterRender = func;
            await RefreshDialog();
        }

        /// <summary>
        /// Use to redraw the dialog if any options are changed.
        /// </summary>
        /// <returns></returns>
        public async Task RefreshDialog()
        {
            var result = OnOptionsChanged?.Invoke();
            if (result != null) await result;
        }
    }

    public class ComponentDialog
    {
        public ComponentAsDialogOptions Options { get; set; }
        public TaskCompletionSource RenderTaskCompletionSource { get; set; }
    }

    public interface IBlazorDialogStore
    {

        void Register(Dialog blazorDialog);
        void Remove(Dialog blazorDialog);
        Dialog GetById(string id);
        int GetVisibleDialogsCount();

        Task RegisterComponentDialog(string id, ComponentDialog compDialog);
        Task UnregisterComponentDialog(string id);

        Dictionary<string, ComponentDialog> GetComponentsAsDialogs();

        event Func<Task> OnComponentAsDialogsChanged;

    }
}
