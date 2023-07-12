using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class ComponentAsDialogOptions
    {
        /// <summary>
        /// The type of the rendered component
        /// </summary>
        public Type ComponentType { get; set; }

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
        public string CssClass { get; set; }

        /// <summary>
        /// Allows you to set the dialog size. Ignored when the dialog is <see cref="Dialog.IsCustom" />.
        /// </summary>
        public DialogSize Size { get; set; } = DialogSize.Normal;
    }

    public class ComponentDialog
    {
        public ComponentAsDialogOptions Options { get; set; }
        public TaskCompletionSource RenderTaskCompletionSource { get; set; }
    }

    public interface IBlazorDialogStore
    {

        void Register(Dialog blazorDialog);
        void Unregister(Dialog blazorDialog);
        Dialog GetById(string id);
        int GetVisibleDialogsCount();

        Task RegisterComponentDialog(string id, ComponentDialog compDialog);
        Task UnregisterComponentDialog(string id);

        Dictionary<string, ComponentDialog> GetComponentsAsDialogs();

        event Func<Task> OnComponentAsDialogsChanged;

    }
}
