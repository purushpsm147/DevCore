using Azure.Identity;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SGRE.TSA.Api.Extensions
{
    public static class KeyVaultExtension
    {
        public static void GetKeysFromVault(this IServiceCollection services, IConfiguration Configuration)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddAzureKeyVault(new Uri(Configuration["KeyVault"]), new DefaultAzureCredential());
            IConfiguration configuration = builder.Build();

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(configuration["BlobAccessKey"]);
                builder.AddServiceBusClient(configuration["ServicveBusConnectionString"]);
            });
        }
    }
}
