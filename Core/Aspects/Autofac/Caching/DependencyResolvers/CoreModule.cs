using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DependencyResolvers
{
	public class CoreModule : ICoreModule
	{
		//servis bagımlılıklarının çözüleceği yer
		public void Load(IServiceCollection serviceCollection)
		{
			serviceCollection.AddMemoryCache();//IMemoryCachein instance'ını olusturuyor
			serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			serviceCollection.AddSingleton<ICacheManager, MemoryCashManager>();
		}
	}
}
