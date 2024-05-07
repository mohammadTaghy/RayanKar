using Application.IRepositoryWrite;
using Application.UseCases.Customers.Command.Uodate;
using Application.UseCases.Customers.Command;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Application.Test.UseCases.Customers.Command
{
    public class UpdateCustomerCommandHandler_Test : CommandUseCaseTestBase<CustomerWrite, ICustomerWriteRepository>
    {
        private readonly UpdateCustomerCommand _customer;
        private readonly UpdateCustomerCommandHandler _handler;
        public UpdateCustomerCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _customer = new UpdateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+989384563280",
                "taghy@gmail.com",
                "IR830120010000001387998021",
                1
            );
            _handler = new UpdateCustomerCommandHandler(_writeRepoMock.Object, _mapper.Object);
            _mapper.Setup(p => p.Map<CustomerWrite>(It.IsAny<UpdateCustomerCommand>())).
                Returns(new CustomerWrite(
                                _customer.Firstname,
                                _customer.Email,
                                 _customer.LastName,
                                _customer.DateOfBirth,
                                _customer.PhoneNumber,
                                _customer.BankAccountNumber
                            )
                );



        }
        [Fact]
        public void UpdateCustomerCommandHandler_GivenWrongPhoneNumber_ShouldArgumentNullException()
        {
            _customer.PhoneNumber = "+980938456321";

            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public void UpdateCustomerCommandHandler_GivenWrongAccountNumber_ShouldArgumentNullException()
        {
            _customer.BankAccountNumber = "IR8301200100";

            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Insert(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public void UpdateCustomerCommandHandler_GivenNullValue_ArgumentNullException()
        {

            Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Update(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public async Task UpdateCustomerCommandHandler_GivenIdNotExist_NotFoundException()
        {
            _writeRepoMock.Setup(p => p.Find(It.IsAny<int>())).Returns(()=>Task.FromResult<CustomerWrite?>(null));

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Update(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public async Task UpdateCustomerCommandHandler_GivenFirstNameLastNamePhoneNumberExist_ValidationException()
        {
            CustomerWrite customer = _mapper.Object.Map<CustomerWrite>(_customer);
            _writeRepoMock.Setup(p => p.Find(It.IsAny<int>())).Returns(Task.FromResult<CustomerWrite?>(customer));
            string message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{_customer.Firstname},{_customer.LastName},{_customer.DateOfBirth}");
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(Task.FromResult(new Tuple<bool,string>(true,message)));

             await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Update(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public async Task UpdateCustomerCommandHandler_GivenEmailExist_ValidationException()
        {
            CustomerWrite customer = _mapper.Object.Map<CustomerWrite>(_customer);
            _writeRepoMock.Setup(p => p.Find(It.IsAny<int>())).Returns(Task.FromResult<CustomerWrite?>(customer));
            string message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{_customer.Email}");
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(Task.FromResult(new Tuple<bool, string>(true, message)));

            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.Update(It.IsAny<CustomerWrite>()), Times.Never);
        }

        [Fact]
        public async Task UpdateCustomerCommandHandler_GivenCorrectValue_ResultOk()
        {
            CustomerWrite customer = _mapper.Object.Map<CustomerWrite>(_customer);
            _writeRepoMock.Setup(p => p.Find(It.IsAny<int>())).Returns(Task.FromResult<CustomerWrite?>(customer));
            _writeRepoMock.Setup(p => p.IsExsists(It.IsAny<CustomerWrite>()))
                .Returns(Task.FromResult(new Tuple<bool, string>(false, string.Empty)));

            CommandResponse<CustomerRead> result = await _handler.Handle(_customer, CancellationToken.None);

            _writeRepoMock.Verify(p => p.Update(It.IsAny<CustomerWrite>()), Times.Once);
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
        }

    }
}
