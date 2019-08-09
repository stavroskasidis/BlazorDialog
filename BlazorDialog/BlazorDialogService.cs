using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class BlazorDialogService : IBlazorDialogService
    {
        private readonly DialogStates _dialogStates;
        private Dictionary<string, TaskCompletionSource<object>> taskCompletionSources = new Dictionary<string, TaskCompletionSource<object>>();

        public BlazorDialogService(DialogStates states)
        {
            this._dialogStates = states;
            this._dialogStates.OnDialogHide += (string dialogId, object result) =>
            {
                taskCompletionSources[dialogId].SetResult(result);
                taskCompletionSources.Remove(dialogId);
            };
        }

        public void HideDialog(string dialogId)
        {
            _dialogStates.HideDialog(dialogId, null);
        }

        public void HideDialog(string dialogId, object result)
        {
            _dialogStates.HideDialog(dialogId, result);
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
            if (taskCompletionSources.ContainsKey(dialogId))
            {
                taskCompletionSources[dialogId].SetCanceled();
            }
            taskCompletionSources[dialogId] = new TaskCompletionSource<object>();
            _dialogStates.ShowDialog(dialogId, input);
            return (TResult)await taskCompletionSources[dialogId].Task;
        }
    }
}
