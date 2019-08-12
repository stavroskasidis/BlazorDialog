using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class DialogContext<TInput>
    {
        public TInput Input { get; protected set; }
        public ModalDialog Dialog { get; protected set; }
        public DialogContext(ModalDialog blazorDialog , TInput input)
        {
            Dialog = blazorDialog;
            Input = input;
        }
    }
}
