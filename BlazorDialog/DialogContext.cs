using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDialog
{
    public class DialogContext<TInput>
    {
        private readonly string _dialogId;
        private readonly BlazorDialogService _dialogService;

        public TInput Input { get; protected set; }

        public DialogContext(string dialogId, BlazorDialogService dialogService, TInput input)
        {
            _dialogId = dialogId;
            _dialogService = dialogService;
            Input = input;
        }

        public Task Show()
        {
            return _dialogService.ShowDialog(this._dialogId);
        }

        public Task Show(TInput input)
        {
            return _dialogService.ShowDialog<TInput>(this._dialogId, input);
        }

        public void Hide<TResult>(TResult result)
        {
            _dialogService.HideDialog(this._dialogId, result);
        }

        public void Hide()
        {
            _dialogService.HideDialog(this._dialogId, null);
        }
    }
}
