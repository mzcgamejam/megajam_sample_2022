using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.GameLift;
using CommonProtocol;
using GameDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GameServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length < 2)
                throw new Exception("The runmode parameter is required.");

            ConfigReader.Instance.Init(args[1]);
            DBEnv.SetUp();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                var featureCollection = context.Features[typeof(IHttpRequestFeature)] as IHttpRequestFeature;
                var url = featureCollection.Path.TrimStart('/');

                if (Enum.TryParse<MessageType>(url, out var messageType))
                {
                    var requestInfo = ProtocolFactory.DeserializeProtocol(messageType, featureCollection.Body);
                    var controller = ControllerFactory.CreateController(messageType, context);

                    var responseInfo = await controller.DoPipeline(requestInfo);
                    await context.Response.Body.WriteAsync(ProtocolFactory.SerializeProtocol(messageType, responseInfo));
                }

            });
        }
    }
}
