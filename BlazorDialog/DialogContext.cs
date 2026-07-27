namespace BlazorDialog
{
    public class DialogContext<TInput>
    {
        public TInput Input { get; protected set; }
        public Dialog Dialog { get; protected set; }
        public DialogContext(Dialog blazorDialog , TInput input)
        {
            Dialog = blazorDialog;
            Input = input;
        }
    }
}
