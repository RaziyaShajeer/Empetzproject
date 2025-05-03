using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
	internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{

			return base.CreateHost(builder);
		}
	}
}
