using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command.Delete
{
    public class DeleteCustomerCommand : IRequest<CommandResponse<bool>>
    {
        public int? Id { get; set; }
        public string? Email { get; set; }

    }
}
