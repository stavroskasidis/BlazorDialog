using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class BlazorDialogService2
    {
        private Dictionary<string, BlazorDialogBase> registeredDialogs = new Dictionary<string, BlazorDialogBase>();

        internal void Register(BlazorDialogBase blazorDialog)
        {
            registeredDialogs[blazorDialog.Id] = blazorDialog;
        }


        internal void Unregister(BlazorDialogBase blazorDialog)
        {
            registeredDialogs.Remove(blazorDialog.Id);
        }

        public void HideDialog(string dialogId)
        {
            registeredDialogs[dialogId].Hide();
        }

        public void HideDialog(string dialogId, object result)
        {
            registeredDialogs[dialogId].Hide(result);
        }

        public async Task ShowDialog(string dialogId)
        {
            await ShowDialog<object>(dialogId, null);
        }

        public async Task<TResult> ShowDialog<TResult>(string dialogId)
        {
            return await ShowDialog<TResult>(dialogId, null);
        }

        public async Task ShowDialog(string dialogId, object input)
        {
            await ShowDialog<object>(dialogId, input);
        }

        public async Task<TResult> ShowDialog<TResult>(string dialogId, object input)
        {
            //if (taskCompletionSources.ContainsKey(dialogId))
            //{
            //    taskCompletionSources[dialogId].SetCanceled();
            //}
            //taskCompletionSources[dialogId] = new TaskCompletionSource<object>();
            //_dialogStates.ShowDialog(dialogId, input);
            //return (TResult)await taskCompletionSources[dialogId].Task;

            return (TResult)await registeredDialogs[dialogId].Show(input);
        }
    }
}
