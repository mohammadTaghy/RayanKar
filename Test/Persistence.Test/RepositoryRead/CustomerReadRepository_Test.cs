using Application.Common;
using Common;
using Domain.ReadEntitis;
using Microsoft.Extensions.Options;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Moq;
using Persistence.RepositoryRead;
using Persistence.Test.RepositoryRead.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData.Query;
using Amazon.Runtime;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.ModelBuilder;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;

namespace Persistence.Test.RepositoryRead
{
    public class CustomerReadRepository_Test : RepositoryReadBase_Test<CustomerRead, CustomerReadRepository>
    {
        public CustomerReadRepository_Test() : base(
            new CustomerReadRepository(
                Options.Create<MongoDatabaseOption>(new MongoDatabaseOption
                {
                    ConnectionString = "mongodb://localhost:27017",
                    DatabaseName = "PersistanceTestDB"
                }),
            new Mock<IRabbitMQUtility>().Object),
                new CustomerRead(
                                        "MohammadTaghy",
                                        new Domain.ValueObject.PhoneNumber("+989384563280"),
                                        "taghy@gmail.com",
                                        new Domain.ValueObject.BankAccountNumber("IR830120010000001387998021"),
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        2
                                    )
            )
        {
        }

        protected override ODataQueryOptions<CustomerRead> GetOdataQueryOption(string url)
        {
            return MakeOdataQueryOption(url);
        }
        protected ODataQueryOptions<CustomerRead> MakeOdataQueryOption(string url)
        {
            IEdmModel edmModel = CreateEdmModel();
            HttpRequest request = CreateHttpRequest(url, edmModel);
            var oDataQueryContext = new ODataQueryContext(edmModel, typeof(CustomerRead), new ODataPath());
            return new ODataQueryOptions<CustomerRead>(oDataQueryContext, request);


        }
        private HttpRequest CreateHttpRequest(string url, IEdmModel edmModel)
        {

            const string routeName = "odata";
            IEdmEntitySet entitySet = edmModel.EntityContainer.FindEntitySet(typeof(CustomerRead).Name);
            ODataPath path = new ODataPath(new EntitySetSegment(entitySet));

            var request = RequestFactory.Create("GET",
                url,
                dataOptions => dataOptions.AddRouteComponents(routeName, edmModel));

            request.ODataFeature().Model = edmModel;
            request.ODataFeature().Path = path;
            request.ODataFeature().RoutePrefix = routeName;
            return request;
        }
        private static IEdmModel CreateEdmModel()
        {
            var builder = new ODataConventionModelBuilder() ;
            
            builder.EntitySet<CustomerRead>(typeof(CustomerRead).Name);
            return builder.GetEdmModel();
        }
        public static class RequestFactory
        {
            /// <summary>
            /// Creates the <see cref="HttpRequest"/> with OData configuration.
            /// </summary>
            /// <param name="method">The http method.</param>
            /// <param name="uri">The http request uri.</param>
            /// <param name="setupAction"></param>
            /// <returns>The HttpRequest.</returns>
            public static HttpRequest Create(string method, string uri, Action<ODataOptions> setupAction)
            {
                HttpContext context = new DefaultHttpContext();
                HttpRequest request = context.Request;

                IServiceCollection services = new ServiceCollection();
                services.Configure(setupAction);
                context.RequestServices = services.BuildServiceProvider();

                request.Method = method;
                var requestUri = new Uri(uri);
                request.Scheme = requestUri.Scheme;
                request.Host = requestUri.IsDefaultPort ? new HostString(requestUri.Host) : new HostString(requestUri.Host, requestUri.Port);
                request.QueryString = new QueryString(requestUri.Query);
                request.Path = new PathString(requestUri.AbsolutePath);

                return request;
            }
        }
    }
}
