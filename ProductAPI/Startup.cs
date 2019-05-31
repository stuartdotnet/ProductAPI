using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using ProductAPI.Data;
using ProductAPI.Model;
using System.Net.Http;

namespace ProductAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddOData();
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); // Has to be 2.1 for now, see https://github.com/Microsoft/aspnet-api-versioning/issues/361
			services.AddSingleton<IProductService, InMemoryProductService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseAuthentication();
			app.UseMvc(b =>
			{
				b.EnableDependencyInjection();
				b.Select().Expand().Filter().OrderBy().MaxTop(5).Count();
				b.MapODataServiceRoute("odata", "odata", GetEdmModel());
			});
		}

		private static IEdmModel GetEdmModel()
		{
			ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
			builder.EntitySet<Product>("Products");
			return builder.GetEdmModel();
		}
	}
}
