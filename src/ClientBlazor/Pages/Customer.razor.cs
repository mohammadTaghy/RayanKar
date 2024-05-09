using ClientBlazor.Model;
using ClientBlazor.Service;
using SharedProject.Customer;

namespace ClientBlazor.Pages
{
    public partial class Customer
    {


        ViewState state { get; set; } = ViewState.List;
        CustomerDto currentCustomer { get; set; } = CustomerDto.EmptyInstance();
        List<CustomerDto> customerList { get; set; } = new List<CustomerDto>();
        protected override Task OnInitializedAsync()
        {
            if (state == ViewState.List)
                GetCustomers();
            return base.OnInitializedAsync();
        }

        async Task GetCustomers()
        {
            QueryResponse<List<CustomerDto>> result = await customerService.GetCustomersAsync();
            if (result.TotalCount == 0)
                state = ViewState.EmtyList;
            customerList = result.Result ?? new List<CustomerDto>();
            await InvokeAsync(StateHasChanged);
        }

        void EditCustomer(CustomerDto customer)
        {
            currentCustomer = customer;
            state = ViewState.Edit;
        }
        void DeleteCustomer(CustomerDto customer)
        {
            currentCustomer = customer;
            state = ViewState.Delete;
        }
        void InsertCustomer()
        {
            currentCustomer = CustomerDto.EmptyInstance();
            state = ViewState.Insert;
        }
        Dictionary<int, string> messageList = new Dictionary<int, string>();
        async Task SendRequest()
        {

            try
            {
                switch (state)
                {
                    case ViewState.Insert:
                        CommandResponse<int> postResponse = await customerService.PostCustomerAsync(currentCustomer);
                        await CheckResponseMessage(postResponse);
                        break;
                    case ViewState.Edit:
                        CommandResponse<CustomerDto> putResponse = await customerService.PutCustomerAsync(currentCustomer);
                        await CheckResponseMessage<CustomerDto>(putResponse);
                        break;
                    case ViewState.Delete:
                        CommandResponse<bool> deleteResponse = await customerService.DeleteCustomerAsync(currentCustomer.Email);
                        await CheckResponseMessage<bool>(deleteResponse);
                        break;
                    default:
                        state = ViewState.List;
                        break;
                }
                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {
                messageList.Add(messageList.Keys.Count + 1, ex.Message);
            }

        }

        private async Task CheckResponseMessage<T>(CommandResponse<T> response)
        {
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                messageList.Add(messageList.Keys.Count + 1, response.Message);
            else
            {
                await GetCustomers();
                state = ViewState.List;
            }
        }

        async Task Remove(int key)
        {
            messageList.Remove(key);
            await InvokeAsync(StateHasChanged);
        }
        void BackToList()
        {
            state = ViewState.List;
        }
    }
    public enum ViewState
    {
        Insert,
        Edit,
        Delete,
        List,
        EmtyList
    }
}
