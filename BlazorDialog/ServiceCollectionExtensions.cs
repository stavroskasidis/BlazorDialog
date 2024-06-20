﻿using BlazorDialog;
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
            services.AddScoped<IBlazorDialogStore, BlazorDialogStore>();
            services.AddScoped<IBlazorDialogService, BlazorDialogService>();
            services.AddScoped<ILocationChangingHandler, LocationChangingHandler>();
            return services;
        }
    }
}
