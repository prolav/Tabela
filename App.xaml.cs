using Tabela.View_PC;

#if WINDOWS
using Microsoft.Maui.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Windowing;
using WinRT.Interop;
#endif

namespace Tabela;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
        InitializeMainPage();
    }

    private void InitializeMainPage()
    {
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            this.MainPage.DisplayAlert("Atenção", $"Está funcionando somente em PC de 24 pol", "OK");
        }
        else if (DeviceInfo.Idiom == DeviceIdiom.Desktop)
        {
            MainPage = new PC_LoginView();
        }
    }

#if WINDOWS
protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState activationState)
{
    var window = base.CreateWindow(activationState);

    void HandlerChanged(object s, EventArgs e)
    {
        var mauiWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;
        if (mauiWindow != null)
        {
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(mauiWindow);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);

            if (appWindow.Presenter is Microsoft.UI.Windowing.OverlappedPresenter presenter)
            {
                presenter.Maximize();
            }

            // Remove o evento para não causar erro ao fechar
            window.HandlerChanged -= HandlerChanged;
        }
    }

    window.HandlerChanged += HandlerChanged;

    return window;
}
#else
    protected override Microsoft.Maui.Controls.Window CreateWindow(IActivationState activationState)
        => base.CreateWindow(activationState);
#endif


}
