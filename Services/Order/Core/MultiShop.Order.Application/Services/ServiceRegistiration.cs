using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Services
{
	// static yapmamızın nedeni diğer class'tan direkt olarak erişebilmek
	public static class ServiceRegistiration
	{
		public static void AddApplicationService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly));
		}
	}
}
