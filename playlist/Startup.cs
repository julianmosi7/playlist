using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using playlist.Services;
using playlistDb;

namespace playlist
{
  public class Startup
  {
    private readonly string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddScoped<playlistContext>();
      services.AddScoped<ValuesService>();
      var builder = new SqliteConnectionStringBuilder(@"Data Source=../../../../playlistDb/chinook.db"); //use relative path here as I don't know how to "Copy always" with dotnet CLI
      var location = System.Reflection.Assembly.GetEntryAssembly().Location;
      string dataDirectory = System.IO.Path.GetDirectoryName(location);
      builder.DataSource = System.IO.Path.Combine(dataDirectory, builder.DataSource);
      var absoluteConnectionString = builder.ToString();
      services.AddDbContext<playlistContext>(options => options.UseSqlite(absoluteConnectionString));

      services.AddCors(options =>
      {
        options.AddPolicy(myAllowSpecificOrigins,
            x => x.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
          );
      });
      services.AddMvc(options => options.EnableEndpointRouting = false);
  }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(myAllowSpecificOrigins);

      app.UseMvc();
    }
  }
}
