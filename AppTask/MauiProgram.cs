using AppTask.DataBase.Repositories;
using AppTask.DataBase;
using Microsoft.Extensions.Logging;
using AppTask.Services;
using AppTask.Views;

namespace AppTask
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

            #region DI- Utilities
            builder.Services.AddScoped<HttpClient>(options =>
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(AppSettings.EndpontAPI);

                return http;
            });
            #endregion


            #region Database Config
            builder.Services.AddDbContext<AppTaskContext>();
            #endregion

            #region Repositories
            builder.Services.AddScoped<IUserModelRepository, UserModelRepository>();
            builder.Services.AddScoped<ITaskModelRepository, TaskModelRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IUserService,UserService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            #endregion

            #region Pages
            builder.Services.AddTransient<StartPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<AddEditTaskPage>();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
