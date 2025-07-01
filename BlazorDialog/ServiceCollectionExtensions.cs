using BlazorDialog;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorDialog(this IServiceCollection services)
        {
            services.TryAddScoped<IBlazorDialogStore, BlazorDialogStore>();
            services.TryAddScoped<IBlazorDialogService, BlazorDialogService>();
            services.TryAddScoped<ILocationChangingHandler, LocationChangingHandler>();
            return services;
        }
    }
}
