using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public abstract class DialogEventArgs
    {
        public ModalDialog Dialog { get; protected set; }
        public DialogEventArgs(ModalDialog dialog)
        {
            Dialog = dialog;
        }
    }

    public class DialogBeforeShowEventArgs : DialogEventArgs
    {
        public DialogBeforeShowEventArgs(ModalDialog dialog) : base(dialog)
        {
        }

        public bool PreventShow { get; set; }
    }

    public class DialogAfterShowEventArgs : DialogEventArgs
    {
        public DialogAfterShowEventArgs(ModalDialog dialog) : base(dialog)
        {
        }
    }

    public class DialogBeforeHideEventArgs : DialogEventArgs
    {
        public DialogBeforeHideEventArgs(ModalDialog dialog) : base(dialog)
        {
        }

        public bool PreventHide { get; set; }
    }

    public class DialogAfterHideEventArgs : DialogEventArgs
    {
        public DialogAfterHideEventArgs(ModalDialog dialog) : base(dialog)
        {
        }
    }
}
