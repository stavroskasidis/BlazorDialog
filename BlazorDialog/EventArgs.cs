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

        public bool PreventShow { get; set; }
    }

    public class DialogAfterShowEventArgs : DialogEventArgs
    {
        public DialogAfterShowEventArgs(Dialog dialog) : base(dialog)
        {
        }
    }

    public class DialogBeforeHideEventArgs : DialogEventArgs
    {
        public DialogBeforeHideEventArgs(Dialog dialog) : base(dialog)
        {
        }

        public bool PreventHide { get; set; }
    }

    public class DialogAfterHideEventArgs : DialogEventArgs
    {
        public DialogAfterHideEventArgs(Dialog dialog) : base(dialog)
        {
        }
    }
}
