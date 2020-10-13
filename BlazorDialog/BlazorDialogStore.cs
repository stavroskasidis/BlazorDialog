using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorDialog
{
    internal class BlazorDialogStore : IBlazorDialogStore
    {
        private Dictionary<string, Dialog> registeredDialogs = new Dictionary<string, Dialog>();

        public Dialog GetById(string id)
        {
            if (registeredDialogs.ContainsKey(id))
            {
                return registeredDialogs[id];
            }

            throw new ArgumentException($"No dialog found for id '{id}'", nameof(id));
        }

        public void Register(Dialog blazorDialog)
        {
            if (blazorDialog?.Id == null)
            {
                throw new ArgumentException("BlazorDialog Id is null", nameof(blazorDialog));
            }
            registeredDialogs[blazorDialog.Id] = blazorDialog;
        }

        public void Unregister(Dialog blazorDialog)
        {
            if (blazorDialog.Id != null && registeredDialogs.ContainsKey(blazorDialog.Id))
            {
                registeredDialogs.Remove(blazorDialog.Id);
            }
        }
    }
}
