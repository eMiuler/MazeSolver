using FileManagementService;
using FileManagementService.Interfaces;
using MazeGenerationService;
using MazeGenerationService.Interfaces;
using MazeSolvingApplication.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace MazeSolvingApplication
{
    public class DIContainer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public DIContainer()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            _configuration = builder.Build();

            var services = ConfigureServices();

            _serviceProvider = services.BuildServiceProvider();
        }

        private IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.Configure<MazeGenerationSettings>(_configuration.GetSection(nameof(MazeGenerationSettings)));

            services.AddSingleton<IFileReader, FileReader>();
            services.AddSingleton<IFileWriter, FileWriter>();
            services.AddSingleton<IMazeGenerator, MazeGeneratorFromFile>();
            services.AddSingleton<IMazeValidator, MazeValidator>();
            services.AddSingleton<IMazeSolver, DfsMazeSolver>();

            return services;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
