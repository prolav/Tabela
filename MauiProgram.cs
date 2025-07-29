using Microsoft.Maui.LifecycleEvents;
using System.Linq;
using Tabela.Data;
using Tabela.Repositories;
using Tabela.View_PC;
using Tabela.View_PC.PC_Partial;
using Tabela.ViewModel_PC;

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

            // PARTIAL DE PC 
            builder.Services.AddSingleton<PC_CadastroClube_Partial>();
            builder.Services.AddSingleton<PC_CadastroJogador_Partial>();
            builder.Services.AddSingleton<PC_ClassificacaoGeral_Partial>();
            builder.Services.AddSingleton<PC_Clube_Partial>();
            builder.Services.AddSingleton<PC_DashBoard_Partial>();
            builder.Services.AddSingleton<PC_HistoricoJogos_Partial>();
            builder.Services.AddSingleton<PC_Jogador_Partial>();
            builder.Services.AddSingleton<PC_NovoCampeonato_Partial>();
            builder.Services.AddSingleton<PC_PlayOff_Partial>();
            builder.Services.AddSingleton<PC_Tabela_Partial>();

            // PAGES DE PC
            builder.Services.AddSingleton<PC_CadastroGeralView>();
            builder.Services.AddSingleton<PC_DashBoardView>();
            builder.Services.AddSingleton<PC_LoginView>();
            builder.Services.AddSingleton<PC_MenuInicialView>();
            // builder.Services.AddSingleton<ClientPage>();

            // DBCONTEXT
            builder.Services.AddSingleton<DbContext>();

            // INTERFACES E REPOSITORIES
             // builder.Services.AddSingleton<ICampeonatoRepository, CampeonatoRepository>();
             // builder.Services.AddSingleton<IClubeRepository, ClubeRepository>();
             // builder.Services.AddSingleton<IFaseRepository, FasesRepository>();
             // builder.Services.AddSingleton<IGrupoRepository, GrupoRepository>();
             // builder.Services.AddSingleton<IJogadorRepository, JogadorRepository>();
             // builder.Services.AddSingleton<IPartidaRepository, PartidaRepository>();
             // builder.Services.AddSingleton<ITimeRepository, TimeRepository>();
             // builder.Services.AddSingleton<IUsuarioRepository, UsuarioRepository>();

            // builder.Services.AddSingleton<INavigationService, MauiNavigationService>();

            // PAGES E VIEWMODELS
            //builder.Services.AddViewToViewModel<PC_CadastroClube_PartialViewModel, PC_CadastroClube_Partial>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            // builder.Services.AddViewModel<ClientListViewModel, ClientListPage>();
            //builder.Services.AddPageToViewModel<PC_LoginViewModel, PC_LoginView>();
            // builder.Services.AddViewModel<ClientViewModel, ClientPage>();

            return builder.Build();
        }

        private static void AddPageToViewModel<TViewModel, TView>(this IServiceCollection services)
            where TView : ContentPage, new()
            where TViewModel : class
        {
            services.AddTransient<TViewModel>();
            services.AddTransient<TView>(s => new TView() { BindingContext = s.GetRequiredService<TViewModel>() });
        }

     
    }
}