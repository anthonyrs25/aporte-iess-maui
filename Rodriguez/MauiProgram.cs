using Microsoft.Extensions.Logging;
using Rodriguez.Services;
using Rodriguez.ViewModels;
using Rodriguez.Views;

namespace Rodriguez;

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

        // Servicios
        builder.Services.AddSingleton<CalculadoraAporteIess>();
        builder.Services.AddSingleton<IAlertaService, AlertaService>();

        // ViewModels y vistas
        builder.Services.AddTransient<VistaInicioViewModel>();
        builder.Services.AddTransient<VistaInicio>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}