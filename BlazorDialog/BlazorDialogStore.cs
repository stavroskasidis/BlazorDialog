using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    internal class BlazorDialogStore : IBlazorDialogStore
    {
        private Dictionary<string, Dialog> registeredDialogs = new Dictionary<string, Dialog>();
        private Dictionary<string, ComponentDialog> registeredComponentDialogs = new Dictionary<string, ComponentDialog>();
        public event Func<Task> OnComponentAsDialogsChanged;

        public Dialog GetById(string id)
        {
            if (registeredDialogs.ContainsKey(id))
            {
                return registeredDialogs[id];
            }

            throw new ArgumentException($"No dialog found for id '{id}'", nameof(id));
        }

        public int GetVisibleDialogsCount()
        {
            return registeredDialogs.Count(x => x.Value.GetVisibility()) +
                  registeredComponentDialogs.Count;
        }

        public void Register(Dialog blazorDialog)
        {
            if (blazorDialog?.Id == null)
            {
                throw new ArgumentException("BlazorDialog Id is null", nameof(blazorDialog));
            }
            registeredDialogs[blazorDialog.Id] = blazorDialog;
        }

        public async Task RegisterComponentDialog(string id, ComponentDialog options)
        {
            registeredComponentDialogs[id] = options;
            if(OnComponentAsDialogsChanged != null)
            {
                await OnComponentAsDialogsChanged.Invoke();
            }
        }

        public void Unregister(Dialog blazorDialog)
        {
            if (blazorDialog.Id != null && registeredDialogs.ContainsKey(blazorDialog.Id))
            {
                registeredDialogs.Remove(blazorDialog.Id);
            }
        }

        public async Task UnregisterComponentDialog(string id)
        {
            if(registeredComponentDialogs.ContainsKey(id))
            {
                registeredComponentDialogs.Remove(id);
                if (OnComponentAsDialogsChanged != null)
                {
                    await OnComponentAsDialogsChanged.Invoke();
                }
            }
        }

        public Dictionary<string, ComponentDialog> GetComponentsAsDialogs()
        {
            return registeredComponentDialogs;
        }
    }
}
