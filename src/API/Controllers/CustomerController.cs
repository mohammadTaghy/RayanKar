using Application.UseCases.Customers.Command.Create;
using Application.UseCases.Customers.Command.Delete;
using Application.UseCases.Customers.Command.Uodate;
using Application.UseCases.Customers.Command;
using Application.UseCases.Customers.Query.Customers;
using Application.UseCases.Customers.Query;
using Asp.Versioning;
using Domain.ReadEntitis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using SharedProject.Customer;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        #region GetApi
        [HttpGet("{id:int}")]
        [ApiVersion("1.0")]
        public async Task<CustomerDto> Customer(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CustomerItemQyery { Id = id }, cancellationToken);
            return result;
        }
        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<QueryResponse<List<CustomerDto>>> Customers(ODataQueryOptions<CustomerRead> options, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CustomerItemListQyery(options), cancellationToken);
            return result;
        }
        #endregion
        #region ChangeApi
        [HttpPost]
        [ApiVersion("1.0")]
        public async Task<CommandResponse<int>> Customers(CreateCustomerCommand createCustomerCommand, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(createCustomerCommand, cancellationToken);
            return result;
        }
        [HttpPut("{id:int}")]
        [ApiVersion("1.0")]
        public async Task<CommandResponse<CustomerRead>> Customers(UpdateCustomerCommand createCustomerCommand, int id, CancellationToken cancellationToken)
        {
            createCustomerCommand.Id = id;
            var result = await _mediator.Send(createCustomerCommand, cancellationToken);
            return result;
        }
        [HttpDelete("{id:int}")]
        [ApiVersion("1.0")]
        public async Task<CommandResponse<bool>> Customers(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Id = id }, cancellationToken);
            return result;
        }
        [HttpDelete]
        [ApiVersion("1.0")]
        public async Task<CommandResponse<bool>> Customers([FromQuery] string email, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand { Email = email }, cancellationToken);
            return result;
        }
        #endregion
    }
}
