using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public interface IBlazorDialogService
    {
        Task HideDialog(string dialogId);
        Task HideDialog(string dialogId, object result);
        Task ShowDialog(string dialogId);
        Task ShowDialog(string dialogId, object input);
        Task<TResult> ShowDialog<TResult>(string dialogId);
        Task<TResult> ShowDialog<TResult>(string dialogId, object input);

        Task ShowComponentAsDialog(ComponentAsDialogOptions options);
        Task<TResult> ShowComponentAsDialog<TResult>(ComponentAsDialogOptions options);
    }
}