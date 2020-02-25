using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Module.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Module.Core
{
    public class ModuleInitializer : IModuleInitializer
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
        }
    }
}
