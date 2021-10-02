using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.DataAccess;

namespace LinenAndBird
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

      //registering a service = telling asp.net how to build a thing
      //services.AddTransient<IConfiguration>()  -> create a new thing anytime someone asks for one
      //services.AddScoped<IConfiguration>() -> create a new thing once per http request. We probably won't ever use this one
      services.AddSingleton<IConfiguration>(Configuration); // -> anytime someone asks for this thing, give them the same copy. Forever. Until the app stops. Similar to static methods.
      
      //we're going to register every repository as a transient
      services.AddTransient<BirdRepository>(); // -> create a new thing anytime someone asks for one
      services.AddTransient<OrdersRepository>(); // -> create a new thing anytime someone asks for one
      services.AddTransient<IHatRepository, HatRepository>(); // -> create a new thing anytime someone asks for one

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "LinenAndBird", Version = "v1" });
      });
    }

     // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
     public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
     {
        if (env.IsDevelopment())
        {
          app.UseDeveloperExceptionPage();
          app.UseSwagger();
          app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinenAndBird v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        //This says to use the endpoints in my controllers
        app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllers();
        });
     }
    }
}
