using System.Threading.Tasks;

namespace BlazorDialog
{
    public interface IBlazorDialogService
    {
        void HideDialog(string dialogId, object result);
        void HideDialog(string dialogId);
        Task ShowDialog(string dialogId);
        Task ShowDialog(string dialogId, object input);
        Task<TResult> ShowDialog<TResult>(string dialogId);
        Task<TResult> ShowDialog<TResult>(string dialogId, object input);
    }
}