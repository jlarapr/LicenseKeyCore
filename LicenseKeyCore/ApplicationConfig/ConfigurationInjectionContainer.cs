using LicenseKeyCore.Algorithm;
using LicenseKeyCore.Factories;
using LicenseKeyCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LicenseKeyCore.ApplicationConfig
{
	public static class ConfigurationInjectionContainer
	{
		public static void ConfigureApplicationServices(IServiceCollection service)
		{
            service.AddScoped<IKeysFactory, KeysFactory>();

            //service.AddSingleton<IRepository, Repository>();
            service.AddSingleton<IAlgorithmDes, AlgorithmDES>();
            service.AddSingleton<IAlgorithmTripleDes, AlgorithmTripleDES>();
            service.AddSingleton<IAlgorithmRijandael, AlgorithmRijndael>();

            service.AddTransient<IRepository, Repository>();
           // service.AddTransient<IAlgorithmDes, AlgorithmDES>();
           // service.AddTransient<IAlgorithmTripleDes, AlgorithmTripleDES>();
            //service.AddTransient<IAlgorithmRijandael, AlgorithmRijndael>();

        }
    }
}
