using Application.IRepositoryRead;
using Application.UseCases.Customers.Query;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Customers.Query
{
    public class CustomerItemQyeryHandler_Test : QueryUseCaseTestBase<CustomerRead, ICustomerReadRepository>
    {
        private readonly CustomerItemQyery _customer;
        private readonly CustomerItemQyeryHandler _handler;
        public CustomerItemQyeryHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _customer = new CustomerItemQyery
            {
                Id = 1
            };
            _handler = new CustomerItemQyeryHandler(_readRepoMock.Object, _mapper.Object);
        }
        [Fact]
        public async Task CustomerItemQyeryHandler_GivenNullParameter_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _readRepoMock.Verify(p => p.FindOne(It.IsAny<int>()), Times.Never);
        }
        [Fact]
        public async Task CustomerItemQyeryHandler_GivenIncorrectId_NotFoundException()
        {
            _readRepoMock.Setup(p => p.FindOne(It.IsAny<int>())).Returns(Task.FromResult<CustomerRead?>(null));

            var exception = await Assert.ThrowsAsync<ValidationException>(
                () => _handler.Handle(_customer, CancellationToken.None));

            Assert.Equal(string.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={_customer.Id}"),
                exception.Message);
        }
        [Fact]
        public async Task CustomerItemQyeryHandler_GivenCorrectId_ResultOK()
        {
            _readRepoMock.Setup(p => p.FindOne(It.IsAny<int>())).Returns(
                Task.FromResult<CustomerRead?>(new CustomerRead(
                                        "Mohammad",
                                        new Domain.ValueObject.PhoneNumber("+989384563280"),
                                        "taghy@gmail.com",
                                        new Domain.ValueObject.BankAccountNumber("IR830120010000001387998021"),
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        1
                                    )
               ));
            _mapper.Setup(p => p.Map<CustomerDto>(It.IsAny<CustomerRead>())).Returns(
                new CustomerDto(
                      1,
                      "Mohammad",
                      "Yami",
                      DateTime.Now.AddYears(-1),
                      "+989384563280",
                      "taghy@gmail.com",
                      "IR830120010000001387998021"
                  )
                );
            CustomerDto result = await _handler.Handle(_customer, CancellationToken.None);
            Assert.Equal("Mohammad", result.Firstname);
        }
    }
}
