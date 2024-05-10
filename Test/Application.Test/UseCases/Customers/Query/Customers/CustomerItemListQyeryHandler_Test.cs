using Application.IRepositoryRead;
using Application.UseCases.Customers.Query;
using Application.UseCases.Customers.Query.Customers;
using Domain.ReadEntitis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OData.UriParser;
using SharedProject.Customer;
using System.Linq.Expressions;
using System.Net.Http;


namespace Application.Test.UseCases.Customers.Query.Customers
{
    public class CustomerItemListQyeryHandler_Test : QueryUseCaseTestBase<CustomerRead, ICustomerReadRepository>
    {
        private readonly CustomerItemListQyeryHandler _handler;


        public CustomerItemListQyeryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _handler = new CustomerItemListQyeryHandler(_readRepoMock.Object, _mapper.Object);

        }
        [Fact]
        public void CustomerItemQyeryHandler_GivenNullParameter_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _readRepoMock.Verify(p => p.ItemList(It.IsAny<ODataQueryOptions<CustomerRead>>()), Times.Never);
        }
        [Fact]
        public async Task UserListQueryHandler_NotErrorWhenTopLessThanZero_QueryTest()
        {
            _readRepoMock.Setup(p => p.ItemList(It.IsAny<ODataQueryOptions<CustomerRead>>()))
                .Returns(Task.FromResult(new Tuple<List<CustomerRead>, int>(new List<CustomerRead>(), 0)));

            QueryResponse<List<CustomerDto>> result =
                await _handler.Handle(
                    GetCustomerListQuery("https://localhost:8080/?$FirstName='qqqq'$top=10&$skip=0&$orderby=Id"),
                    CancellationToken.None);

            _readRepoMock.Verify(p => p.ItemList(It.IsAny<ODataQueryOptions<CustomerRead>>()), Times.Once);
            Assert.Equal(0, result.TotalCount);
        }
        [Fact]
        public async Task UserListQueryHandler_Succes_QueryTest()
        {
            _readRepoMock.Setup(p => p.ItemList(It.IsAny<ODataQueryOptions<CustomerRead>>()))
                .Returns(Task.FromResult(new Tuple<List<CustomerRead>, int>(new List<CustomerRead>
                {
                    new CustomerRead(
                                        "Mohammad",
                                        "taghy@gmail.com",
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        1,
                                        "+989384563280",
                                        "IR830120010000001387998021"
                                    )
                }, 1)));
            QueryResponse<List<CustomerDto>> result =
                await _handler.Handle(GetCustomerListQuery("https://localhost:8080/?$top=10&$skip=0&$orderby=Id"), CancellationToken.None);

            _readRepoMock.Verify(p => p.ItemList(It.IsAny<ODataQueryOptions<CustomerRead>>()), Times.Once);
            Assert.Equal(1, result.TotalCount);
        }
        private CustomerItemListQyery GetCustomerListQuery(string url)
        {
            return new CustomerItemListQyery(MakeOdataQueryOption(url));
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
            var builder = new ODataConventionModelBuilder();

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
