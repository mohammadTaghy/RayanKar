using Application.UseCases.Customers.Command.Create;
using Application.UseCases.Customers.Command;
using Common;
using Domain.Entities;
using Domain.WriteEntities;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Application.IRepositoryWrite;

namespace Application.Test.UseCases.Customers.Command
{
    public class CreateCustomerCommandHandler_Test : CommandUseCaseTestBase<CustomerWrite, ICustomerWriteRepository>
    {
        private readonly CreateCustomerCommand _customer;
        private readonly CreateCustomerCommandHandler _handler;
        public CreateCustomerCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _customer = new CreateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+989384563280",
                "taghy@gmail.com",
                "IR830120010000001387998021"
            );
            _handler = new CreateCustomerCommandHandler(_writeRepoMock.Object, _mapper.Object);
            _mapper.Setup(p => p.Map<CustomerWrite>(It.IsAny<CreateCustomerCommand>()))
                .Returns(new CustomerWrite(
                                _customer.Firstname,
                                new Domain.ValueObject.PhoneNumber(_customer.PhoneNumber),
                                _customer.Email,
                                new Domain.ValueObject.BankAccountNumber(_customer.BankAccountNumber),
                                 _customer.LastName,
                                _customer.DateOfBirth
                            )
                );

        }
        [Fact]
        public void CreateCustomerCommandHandler_GivenNullRequest_ShouldArgumentNullException()
        {

             Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Never);
        }
       
        [Fact]
        public async Task CreateCustomerCommandHandler_CheckExistFirstNameLastNamePhoneNumber_ValidException()
        {
            string message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{_customer.Firstname},{_customer.LastName},{_customer.DateOfBirth}");
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(new Tuple<bool,string>(true, message));

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public async Task CreateCustomerCommandHandler_CheckExistEmail_ValidException()
        {
            string message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{_customer.Email}");
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(new Tuple<bool, string>(true, message));

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Never);
        }

        [Fact]
        public async Task CreateCustomerCommandHandler_CorrectRequest_OkResult()
        {
            string message = "";
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(new Tuple<bool, string>(false, message));

            CommandResponse<int> result = await _handler.Handle(_customer, CancellationToken.None);

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
