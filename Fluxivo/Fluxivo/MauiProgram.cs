using Fluxivo.Shared.Services;
using Fluxivo.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;

namespace Fluxivo;

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
            });

        builder.Services.AddSingleton<IFormFactor, FormFactor>();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddMauiBlazorWebView();

        builder.Services.AddHttpClient("FluxivoAPI", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7158/"); // Beispiel: Port anpassen!
        });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}