using ClientBlazor.Model;
using Newtonsoft.Json;
using SharedProject.Customer;
using System.Text;

namespace ClientBlazor.Service
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerAsync(int id);
        Task<QueryResponse<List<CustomerDto>>?> GetCustomersAsync();
        Task<CommandResponse<int>?> PostCustomerAsync(CustomerDto customer);
        Task<CommandResponse<CustomerDto>?> PutCustomerAsync(CustomerDto customer);
        Task<CommandResponse<bool>?> DeleteCustomerAsync(string email);
    }
    public class CustomerService:ICustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
           

        }
        public async Task<QueryResponse<List<CustomerDto>>?> GetCustomersAsync()
        {
            var response = await _httpClient.GetAsync($"api/Customer/customers?api-version=1&$top=10&$skip=0");
            return await GetResponseDeserilizes<QueryResponse<List<CustomerDto>>>(response);

        }

        public async Task<CustomerDto?> GetCustomerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Customer/Customer/{id}");
            return await GetResponseDeserilizes<CustomerDto>(response);

        }
        public async Task<CommandResponse<int>?> PostCustomerAsync(CustomerDto customer)
        {
            if (!CustomerValidate.CommonValidate(customer, out string errorMessage))
                return new CommandResponse<int>(System.Net.HttpStatusCode.BadRequest, errorMessage, 0);
            var response = await _httpClient.PostAsync($"api/Customer/customers?api-version=1",
                GetRequestContent(customer));
            return await GetResponseDeserilizes<CommandResponse<int>>(response);
        }


        public async Task<CommandResponse<CustomerDto>?> PutCustomerAsync(CustomerDto customer)
        {
            if (!CustomerValidate.CommonValidate(customer, out string errorMessage))
                return new CommandResponse<CustomerDto>(System.Net.HttpStatusCode.BadRequest, errorMessage, CustomerDto.EmptyInstance());
            var response = await _httpClient.PutAsync($"/api/Customer/customers/{customer.Id}?api-version=1", 
                GetRequestContent(customer));
            return await GetResponseDeserilizes<CommandResponse<CustomerDto>>(response);
        }
        public async Task<CommandResponse<bool>?> DeleteCustomerAsync(string email)
        {
            var response = await _httpClient.DeleteAsync($"/api/Customer/Customers?api-version=1&email={email}");
            return await GetResponseDeserilizes<CommandResponse<bool>>(response);
        }
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
        private static async Task<T?> GetResponseDeserilizes<T>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseBody);
        }
    }
}
