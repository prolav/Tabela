using Tabela.Repositories;

namespace Tabela.Data;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        var conn = AppDatabase.GetConnection();
        builder.Services.AddSingleton<IJogadorRepository>(new JogadorRepository(conn));
        builder.Services.AddSingleton<IClubeRepository>(new ClubeRepository(conn));

        return builder.Build();
    }
}