using GokeBlazor.Client.Services;
using GokeBlazor.Client.Services.AspnetIdentity.Contracts;
using GokeBlazor.Client.Services.AspnetIdentity.Implementations;
using GokeBlazor.Client.States;
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

namespace GokeBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var identity = builder.Configuration["Identity"];
            bool IsAspnetIdentity = identity == "AspnetIdentity";

            builder.RootComponents.Add<App>("#app");


            if (!IsAspnetIdentity)
            {
                builder.Services.AddHttpClient("GokeBlazor.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

                // Supply HttpClient instances that include access tokens when making requests to the server project
                builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("GokeBlazor.ServerAPI"));

                builder.Services.AddApiAuthorization();

                builder.Services.AddSingleton<IIdentitySetting, IdentityServerSetting>();

            }
            else
            {
                builder.Services.AddOptions();
                builder.Services.AddAuthorizationCore();
                builder.Services.AddScoped<IdentityAuthenticationStateProvider>();
                builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());
                builder.Services.AddScoped<IAuthorizeApi, AuthorizeApi>();

                builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

                builder.Services.AddSingleton<IIdentitySetting, AspnetIdentitySetting>();

            }

            await builder.Build().RunAsync();
        }
    }
}
