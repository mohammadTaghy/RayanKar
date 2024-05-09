using Api.BDDTest.Common;
using API;
using Application.UseCases.Customers.Command;
using Application.UseCases.Customers.Command.Create;
using Application.UseCases.Customers.Command.Uodate;
using Application.UseCases.Customers.Query;
using Newtonsoft.Json;
using SharedProject.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Api.BDDTest.StepDefinitions
{
    [Binding]
    public class CustomerManagerStepDefinition
    {
        private readonly HttpClient _client;
        private readonly Dictionary<int, string> systemErrorCodes;
        private HttpResponseMessage? response;
        private int userId = 0;

        public CustomerManagerStepDefinition(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            this.systemErrorCodes = new Dictionary<int, string>();
        }

        
        [Given(@"system errors code are folowing")]
        public void GivenSystemErrorCodesAreFollowing(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                systemErrorCodes.Add(int.Parse(row[0]), row[1]);
            }
        }

        
        [Given(@"Exist ""([^""]*)"" customers")]
        public async Task GivenPlatformHasCustomersAsync(string p0)
        {

            HttpResponseMessage responseGet = await _client.GetAsync($"/api/Customer/Customers?api-version=1&$top=10&$skip=0");
            QueryResponse<List<CustomerDto>> queryResponse =
                JsonConvert.DeserializeObject<QueryResponse<List<CustomerDto>>>(
                   await responseGet.Content.ReadAsStringAsync()
                    ) ?? new QueryResponse<List<CustomerDto>>(null,0,false,"");

            Assert.Equal(p0, queryResponse.TotalCount.ToString());
        }

        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        [When(@"user want to create a customer by below data by sending ""(.*)""")]
        public async Task WhenUserCreatesACustomerWithFollowingDataBySending(string p0, Table table)
        {
            IEnumerable<CreateCustomerCommand> createCustomerCommandList = table.CreateSet<CreateCustomerCommand>();


            response = await _client.PostAsync($"/api/Customer/customers?api-version=1",
                Utilities.GetRequestContent(createCustomerCommandList.First()));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                CommandResponse<int> responseContent =
                    JsonConvert.DeserializeObject<CommandResponse<int>>(response.Content.ReadAsStringAsync().Result) ??
                    new CommandResponse<int>(HttpStatusCode.BadRequest, "", 0);
                userId = responseContent.Result;
                await Task.Delay(5000);

            }
        }

        /// <exception cref="UriFormatException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        [Then(@"user can filter customers by follow data and get ""([^""]*)"" data")]
        public async Task ThenUserCanLookupAllCustomersAndFilterByBelowPropertiesAndGetRecords(string p0, Table table)
        {
            IEnumerable<CreateCustomerCommand> createCustomerCommandList = table.CreateSet<CreateCustomerCommand>();
            CreateCustomerCommand createCustomerCommand = createCustomerCommandList.First();

            string url=
                $"{_client.BaseAddress}api/Customer/customers?api-version=1&$top=10&$skip=0" +
                $"&$filter={nameof(CreateCustomerCommand.Firstname)} eq '{createCustomerCommand.Firstname}'  and " +
                $" {nameof(CreateCustomerCommand.LastName)} eq '{createCustomerCommand.LastName}'  and " +
                $"{nameof(CreateCustomerCommand.BankAccountNumber)} eq '{createCustomerCommand.BankAccountNumber}'  and " +
                $"{nameof(CreateCustomerCommand.Email)} eq '{createCustomerCommand.Email}'";
            HttpResponseMessage responseGet = await _client.GetAsync(url);
            QueryResponse<List<CustomerDto>>? queryResponse =
                JsonConvert.DeserializeObject<QueryResponse<List<CustomerDto>>>(
                   await responseGet.Content.ReadAsStringAsync()) ?? 
                   new QueryResponse<List<CustomerDto>>(null,0,false,"");
            Assert.Equal(p0, queryResponse.TotalCount.ToString());
        }

        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        [Then(@"user must receive error codes")]
        public void ThenUserMustReceiveErrorCodes(Table table)
        {
            HashSet<int> responseCode = new HashSet<int>() { int.Parse(table.Rows[0][0]) };

            Assert.True(responseCode.First() == (int)response.StatusCode);
        }
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        [When(@"user edit customer with new data")]
        public async Task WhenUserEditCustomerWithNewData(Table table)
        {
            IEnumerable<UpdateCustomerCommand> updateCustomerCommands = table.CreateSet<UpdateCustomerCommand>();

            response = await _client.PutAsync($"/api/Customer/customers/{userId}?api-version=1", Utilities.GetRequestContent(updateCustomerCommands.First()));
            await Task.Delay(5000);
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        /// <exception cref="Xunit.Sdk.TrueException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        /// <exception cref="AggregateException"></exception>
        [When(@"user delete customer with email of ""([^""]*)""")]
        public async Task WhenUserDeleteCustomerWithEmailOf(string email)
        {
            HttpResponseMessage responseGet = await _client.DeleteAsync($"/api/Customer/Customers?api-version=1&email={email}");
            CommandResponse<bool>? result = JsonConvert.DeserializeObject<CommandResponse<bool>>(
                await responseGet.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, responseGet.StatusCode);
            Assert.True(result?.Result);
        }

        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="TaskCanceledException"></exception>
        /// <exception cref="AggregateException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Xunit.Sdk.EqualException"></exception>
        /// <exception cref="OverflowException"></exception>
        [Then(@"user can query to get all customers and get ""([^""]*)"" records")]
        public async Task ThenUserCanQueryToGetAllCustomersAndGetRecords(string count)
        {

            HttpResponseMessage responseGet = await _client.GetAsync($"/api/Customer/customers?api-version=1");
            QueryResponse<List<CustomerDto>>? queryResponse = JsonConvert.DeserializeObject<QueryResponse<List<CustomerDto>>>(
                await responseGet.Content.ReadAsStringAsync());

            Assert.Equal<long>(long.Parse(count), queryResponse?.TotalCount??0);

        }

    }
}
