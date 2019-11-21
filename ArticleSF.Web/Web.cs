namespace ArticleSF.Web
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
    using Microsoft.ServiceFabric.Services.Communication.Runtime;
    using Microsoft.ServiceFabric.Services.Runtime;
    using System;
    using System.Collections.Generic;
    using System.Fabric;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;

    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class Web : StatelessService
    {
        public Web(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(
                        serviceContext,
                        "ServiceEndpointHttps",
                        (url, listener) =>
                        {
                            ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                            return new WebHostBuilder()
                                .UseKestrel(opt =>
                                {
                                    int port = serviceContext.CodePackageActivationContext.GetEndpoint("ServiceEndpointHttps").Port;
                                    opt.Listen(IPAddress.IPv6Any, port, listenOptions =>
                                    {
                                        listenOptions.UseHttps(GetHttpsCertificateFromStore());
                                        //listenOptions.NoDelay = true;
                                    });
                                })
                                .ConfigureServices(
                                    services => services
                                        .AddSingleton<HttpClient>(new HttpClient())
                                        .AddSingleton<FabricClient>(new FabricClient())
                                        .AddSingleton<StatelessServiceContext>(serviceContext))
                                .UseContentRoot(Directory.GetCurrentDirectory())
                                .UseStartup<Startup>()
                                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                .UseUrls(url)
                                .Build();
                        }))
                    //new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    //{
                    //    ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                    //    return new WebHostBuilder()
                    //                .UseKestrel()
                    //                .ConfigureServices(
                    //                    services => services
                    //                        .AddSingleton<StatelessServiceContext>(serviceContext))
                    //                .UseContentRoot(Directory.GetCurrentDirectory())
                    //                .UseStartup<Startup>()
                    //                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                    //                .UseUrls(url)
                    //                .Build();
                    //}))
                    //new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                    //{
                    //    ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                    //    return new WebHostBuilder()
                    //                .UseKestrel()
                    //                .ConfigureServices(
                    //                    services => services
                    //                        .AddSingleton<StatelessServiceContext>(serviceContext))
                    //                .UseContentRoot(Directory.GetCurrentDirectory())
                    //                .UseStartup<Startup>()
                    //                .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                    //                .UseUrls(url)
                    //                .Build();
                    //}))
                //new ServiceInstanceListener(serviceContext =>
                //    new KestrelCommunicationListener(serviceContext, "ServiceEndpoint", (url, listener) =>
                //    {
                //        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                //        return new WebHostBuilder()
                //                    .UseKestrel()
                //                    //.UseHttpSys()
                //                    .ConfigureServices(
                //                        services => services
                //                            .AddSingleton<StatelessServiceContext>(serviceContext))
                //                    .UseContentRoot(Directory.GetCurrentDirectory())
                //                    .UseStartup<Startup>()
                //                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                //                    .UseUrls(url)
                //                    .Build();
                //    }))
                //new ServiceInstanceListener(serviceContext =>
                //    new KestrelCommunicationListener(serviceContext, "ServiceEndpointHttps", (url, listener) =>
                //    {
                //        ServiceEventSource.Current.ServiceMessage(serviceContext, $"Starting Kestrel on {url}");

                //        return new WebHostBuilder()
                //                    .UseKestrel(opt =>
                //                    {
                //                        int port = serviceContext.CodePackageActivationContext.GetEndpoint("EndpointHttps").Port;
                //                        opt.Listen(IPAddress.IPv6Any, port, listenOptions =>
                //                        {
                //                            listenOptions.UseHttps(GetHttpsCertificateFromStore());
                //                            //listenOptions.NoDelay = true;
                //                            //listenOptions.NoDelay = true;
                //                        });
                //                    })
                //                    //.UseHttpSys()
                //                    .ConfigureServices(
                //                        services => services
                //                            .AddSingleton<StatelessServiceContext>(serviceContext))
                //                    .UseContentRoot(Directory.GetCurrentDirectory())
                //                    .UseStartup<Startup>()
                //                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.UseUniqueServiceUrl)
                //                    .UseUrls(url)
                //                    .Build();
                //    }))

            };
        }

        private X509Certificate2 GetHttpsCertificateFromStore()
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates;
                //var currentCerts = certCollection.Find(X509FindType.FindBySubjectDistinguishedName, "CN=eastus.cloudapp.azure.com", false);
                var currentCerts = certCollection.Find(X509FindType.FindBySubjectDistinguishedName, "CN=localhost", false);

                if (currentCerts.Count == 0)
                {
                    throw new Exception("Https certificate is not found.");
                }

                return currentCerts[0];
            }
        }
    }
}
