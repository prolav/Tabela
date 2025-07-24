using Microsoft.Maui.LifecycleEvents;
using System.Linq;

#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif

#if MACCATALYST
using UIKit;
using CoreGraphics;
using ObjCRuntime;
#endif

namespace Tabela
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.ConfigureLifecycleEvents(events =>
            {
#if WINDOWS
                events.AddWindows(w =>
                {
                    w.OnWindowCreated(window =>
                    {
                        var nativeWindow = window.Handler.PlatformView as Microsoft.UI.Xaml.Window;

                        IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);
                        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hwnd);
                        AppWindow appWindow = AppWindow.GetFromWindowId(wndId);

                        appWindow.TryMaximize(); // Maximiza no Windows
                    });
                });
#endif

#if MACCATALYST
                events.AddiOS(w =>
                {
                    w.FinishedLaunching((app, options) =>
                    {
                        var windowScene = UIApplication.SharedApplication
                            .ConnectedScenes
                            .OfType<UIWindowScene>()
                            .FirstOrDefault();

                        var window = windowScene?
                            .Windows
                            .FirstOrDefault();

                        if (window?.WindowScene?.SizeRestrictions != null)
                        {
                            window.WindowScene.SizeRestrictions.MaximumSize = new CGSize(1500, 1500);
                            window.WindowScene.SizeRestrictions.MinimumSize = new CGSize(2000, 800);
    
                            // Emula clique no botão de maximizar (apenas para macOS Catalyst)
                            window.RootViewController?.View?.Window?.PerformSelector(new Selector("zoom:"), null, 0);
                        }

                        return true;
                    });
                });
#endif
            });

            return builder.Build();
        }
    }
}