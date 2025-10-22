using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public abstract class DialogEventArgs
    {
        public Dialog Dialog { get; protected set; }
        public DialogEventArgs(Dialog dialog)
        {
            Dialog = dialog;
        }
    }

    public class DialogBeforeShowEventArgs : DialogEventArgs
    {
        public DialogBeforeShowEventArgs(Dialog dialog) : base(dialog)
        {
        }

        //public bool PreventShow { get; set; }
    }

    public class DialogAfterShowEventArgs : DialogEventArgs
    {
        public DialogAfterShowEventArgs(Dialog dialog) : base(dialog)
        {
        }
    }

    public class DialogBeforeHideEventArgs : DialogEventArgs
    {
        public DialogBeforeHideEventArgs(Dialog dialog, object? result, bool triggeredFromKeyboard, bool triggeredFromCloseButton) : base(dialog)
        {
            Result = result;
            TriggeredFromKeyboard = triggeredFromKeyboard;
            TriggeredFromCloseButton = triggeredFromCloseButton;
        }

        /// <summary>
        /// If set to true, the dialog hide action will be prevented.
        /// </summary>
        public bool PreventHide { get; set; }

        /// <summary>
        /// The dialog result if any.
        /// </summary>
        public object? Result { get; }

        /// <summary>
        /// Gets a value indicating whether the action was triggered by a keyboard input.
        /// </summary>
        public bool TriggeredFromKeyboard { get; }

        /// <summary>
        /// Gets a value indicating whether the action was triggered by the close button.
        /// </summary>
        public bool TriggeredFromCloseButton { get; }
    }

    public class DialogAfterHideEventArgs : DialogEventArgs
    {
        public DialogAfterHideEventArgs(Dialog dialog, bool triggeredFromKeyboard, bool triggeredFromCloseButton) : base(dialog)
        {
            TriggeredFromKeyboard = triggeredFromKeyboard;
            TriggeredFromCloseButton = triggeredFromCloseButton;
        }
        /// <summary>
        /// Gets a value indicating whether the action was triggered by a keyboard input.
        /// </summary>
        public bool TriggeredFromKeyboard { get; }
        /// <summary>
        /// Gets a value indicating whether the action was triggered by the close button.
        /// </summary>
        public bool TriggeredFromCloseButton { get; }
    }
}
