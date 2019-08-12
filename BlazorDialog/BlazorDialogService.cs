using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class BlazorDialogService : IBlazorDialogService
    {
        private Dictionary<string, ModalDialog> registeredDialogs = new Dictionary<string, ModalDialog>();

        public void Register(ModalDialog blazorDialog)
        {
            if(blazorDialog?.Id == null)
            {
                throw new ArgumentException("BlazorDialog Id is null", nameof(blazorDialog));
            }
            registeredDialogs[blazorDialog.Id] = blazorDialog;
        }

        public void Unregister(ModalDialog blazorDialog)
        {
            if (blazorDialog.Id != null && registeredDialogs.ContainsKey(blazorDialog.Id))
            {
                registeredDialogs.Remove(blazorDialog.Id);
            }
        }

        public async Task HideDialog(string dialogId)
        {
            await registeredDialogs[dialogId].Hide();
        }

        public async Task HideDialog(string dialogId, object result)
        {
            await registeredDialogs[dialogId].Hide(result);
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
            return await registeredDialogs[dialogId].Show<TResult>(input);
        }
    }
}
