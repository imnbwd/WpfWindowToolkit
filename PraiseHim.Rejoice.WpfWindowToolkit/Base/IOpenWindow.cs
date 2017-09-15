namespace PraiseHim.Rejoice.WpfWindowToolkit.Base
{
    public interface IOpenWindow
    {
        OpenWindowInfo OpenWindow { get; set; }

        void ShowWindow();
    }
}