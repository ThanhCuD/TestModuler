using Microsoft.Extensions.DependencyInjection;
using Module.Infrastructure;
using Newtonsoft.Json;
using Module.Infrastructure.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Module.WebHost.Extentions
{
    public static class ServiceCollectionExtensions
    {
        private static readonly IModuleConfigurationManager _moduleCfManager = new ModuleConfigurationManager();
        public static IServiceCollection AddModules(this IServiceCollection services, string contentRootPath)
        {
            const string moduleManifestName = "module.json";
            var modulesFolder = Path.Combine(contentRootPath, "Modules");
            foreach (var item in _moduleCfManager.GetModules())
            {
                var moduleFolder = new DirectoryInfo(Path.Combine(modulesFolder, item.Id));
                var moduleManifestPath = Path.Combine(moduleFolder.FullName, moduleManifestName);
                if (!File.Exists(moduleManifestPath))
                {
                    throw new Exception($"The manifest for the module '{moduleFolder.Name}' is not found.");
                }
                using(var reader = new StreamReader(moduleManifestPath))
                {
                    string content = reader.ReadToEnd();
                    dynamic moduleMetaData = JsonConvert.DeserializeObject(content);
                    item.Name = moduleMetaData.name;
                    item.IsBundledWithHost = moduleMetaData.isBundledWithHost;
                }
                item.Assembly = Assembly.Load(new AssemblyName(moduleFolder.Name));
                GlobalConfiguration.Modules.Add(item);
            }
            return services;
        }
    }
}
