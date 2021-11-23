using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlateForm.ApplicationLogic;
using PlateForm.Repository;
using PlateForm.Repository.ApiClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlateForm.WEB
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddTransient<IProjectsScreenUseCases, ProjectsScreenUseCases>();
            builder.Services.AddTransient<ITicketsScreenUseCases, TicketsScreenUseCases>();
            builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
            builder.Services.AddTransient<ITicketRepository, TicketRepository>();

            builder.Services.AddSingleton<IWebApiExecuter>( new WebApiExecuter("https://localhost:44378", new HttpClient()) );

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
