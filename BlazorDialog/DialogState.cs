using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class DialogStates
    {
        internal Dictionary<string, object> Inputs { get; set; } = new Dictionary<string, object>();
        internal Dictionary<string, bool> IsShowing { get; set; } = new Dictionary<string, bool>();
        internal void ShowDialog(string dialogId,object input)
        {
            Inputs[dialogId] = input;
            IsShowing[dialogId] = true;
            NotifyDialogStateChange(dialogId);
        }

        internal void HideDialog(string dialogId, object result)
        {
            IsShowing[dialogId] = false;
            NotifyDialogStateChange(dialogId);
            NotifyDialogHidden(dialogId, result);
        }

        internal Action<string, object> OnDialogHide;
        protected void NotifyDialogHidden(string dialogId, object result) => OnDialogHide?.Invoke(dialogId, result);
        internal Action OnDialogStateChange;
        protected void NotifyDialogStateChange(string dialogId) => OnDialogStateChange?.Invoke();
    }
}
