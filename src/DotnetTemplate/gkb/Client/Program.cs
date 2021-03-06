using gkb.Client.Services;
using gkb.Client.Services.AspnetIdentity.Contracts;
using gkb.Client.Services.AspnetIdentity.Implementations;
using gkb.Client.States;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gkb.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var identity = builder.Configuration["Identity"];

            builder.RootComponents.Add<App>("#app");

            switch (identity)
            {
                case "AspnetIdentity":
                    AddAspnetIdentity(builder); break;
                case "IdentityServer":
                    AddIdentityServer(builder); break;
                default:
                    break;
            };

            await builder.Build().RunAsync();
        }

        private static void AddAspnetIdentity(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
            builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<IIdentitySetting, AspnetIdentitySetting>();
        }

        private static void AddIdentityServer(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient("gkb.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("gkb.ServerAPI"));

            // builder.Services.AddApiAuthorization();
            builder.Services.AddApiAuthorization(
                option =>
                {
                    option.UserOptions.RoleClaim = "role";
                });

            builder.Services.AddSingleton<IIdentitySetting, IdentityServerSetting>();
        }
    }
}
