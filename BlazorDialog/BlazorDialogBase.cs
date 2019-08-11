using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public abstract class BlazorDialogBase : ComponentBase
    {
        [Parameter] public string Id { get; protected set; }
        [Parameter] protected bool IsShowing { get; set; }
        

        public async Task Show()
        {
            this.IsShowing = true;
            await Invoke(() => StateHasChanged());

        }

        //public Task Show(TInput input)
        //{
        //    return dialogService.ShowDialog<TInput>(this.Id, input);
        //}

        //public void Hide<TResult>(TResult result)
        //{
        //    dialogService.HideDialog(this.Id, result);
        //}

        //public void Hide()
        //{
        //    dialogService.HideDialog(this.Id);
        //}
    }
}
