using MedicalInstitutions.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalInstitutions.Startup))]
namespace MedicalInstitutions
{
    public partial class Startup
    {
		public void ConfigureServices(IServiceCollection services)
		{
			
		}

		public void Configuration(IAppBuilder app)
        {
	        ConfigureAuth(app);
        }
    }
}
