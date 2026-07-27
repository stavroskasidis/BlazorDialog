namespace BlazorDialog
{
    public interface ILocationChangingHandler
    {
        void RegisterLocationChangingHandler(Dialog dialog);
        void RemoveLocationChangingHandler(Dialog dialog);
    }
}
