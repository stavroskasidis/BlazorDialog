using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class BlazorDialogService : IBlazorDialogService
    {
        private readonly IBlazorDialogStore _blazorDialogStore;

        public BlazorDialogService(IBlazorDialogStore blazorDialogStore)
        {
            _blazorDialogStore = blazorDialogStore;
        }

        public async Task HideDialog(string dialogId)
        {
            await _blazorDialogStore.GetById(dialogId).Hide();
        }

        public async Task HideDialog(string dialogId, object result)
        {
            await _blazorDialogStore.GetById(dialogId).Hide(result);
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
            return await _blazorDialogStore.GetById(dialogId).Show<TResult>(input);
        }
    }
}
