using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDialog
{
    public interface IBlazorDialogStore
    {

        void Register(Dialog blazorDialog);
        void Unregister(Dialog blazorDialog);
        Dialog GetById(string id);
        int GetVisibleDialogsCount();
    }
}
