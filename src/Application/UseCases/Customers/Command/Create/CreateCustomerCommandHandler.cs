using Application.IRepositoryWrite;
using AutoMapper;
using Domain.Entities;
using Domain.WriteEntities;
using SharedProject;
using SharedProject.Customer;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Application.UseCases.Customers.Command.Create
{
    public class CreateCustomerCommandHandler : BaseCommandHandler<CreateCustomerCommand, CommandResponse<int>, ICustomerWriteRepository>
    {
        public CreateCustomerCommandHandler(ICustomerWriteRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
        public override async Task<CommandResponse<int>> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            CheckIfNull(request);
            ValidateRequest(request);
            CustomerWrite customer = await InsertCustomer(request);

            return new CommandResponse<int>(
                HttpStatusCode.OK,
                string.Format(CommonMessage.InsertedSucceed, nameof(CustomerWrite)),
                 customer.Id);
        }

        private void CheckIfNull(CreateCustomerCommand request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
            }
        }
        private void ValidateRequest(CreateCustomerCommand request)
        {
            request.FillCustomerDto();
            if (!CustomerValidate.CommonValidate(request.CustomerDto, out string validateMessage))
            {
                throw new ValidationException(validateMessage);
            }
        }

        private async Task<CustomerWrite> InsertCustomer(CreateCustomerCommand request)
        {
            CustomerWrite customer = _mapper.Map<CustomerWrite>(request);
            await CheckCustomerIsDuplicate(customer);
            await _writeRepo.Insert(customer);

            return customer;
        }

        private async Task CheckCustomerIsDuplicate(CustomerWrite customer)
        {
            Tuple<bool, string> existUser = await _writeRepo.IsExsists(customer);
            if (existUser.Item1)
                throw new ValidationException(existUser.Item2);
        }

    }
}
