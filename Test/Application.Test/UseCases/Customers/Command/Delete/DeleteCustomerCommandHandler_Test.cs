using Application.IRepositoryWrite;
using Application.UseCases.Customers.Command.Delete;
using Application.UseCases.Customers.Command;
using Common;
using Domain.Entities;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.ValueObject;

namespace Application.Test.UseCases.Customers.Command.Delete
{
    public class DeleteCustomerCommandHandler_Test : CommandUseCaseTestBase<CustomerWrite, ICustomerWriteRepository>
    {
        private readonly DeleteCustomerCommand _customer;
        private readonly DeleteCustomerCommandHandler _handler;
        public DeleteCustomerCommandHandler_Test(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _customer = new DeleteCustomerCommand
            {
                Id = 1
            };
            _handler = new DeleteCustomerCommandHandler(_writeRepoMock.Object, _mapper.Object);
        }
        [Fact]
        public void DeleteCustomerCommandHandler_GivenNullId_ArgumentNullException()
        {

            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));

            _writeRepoMock.Verify(p => p.DeleteItem(It.IsAny<CustomerWrite>()), Times.Never);
        }
        [Fact]
        public async Task DeleteCustomerCommandHandler_GivenIdNotExist_NotFoundException()
        {
            _writeRepoMock.Setup(p => p.Find(It.IsAny<Expression<Func<CustomerWrite, bool>>>()))
                .Returns(() => Task.FromResult<CustomerWrite?>(null));

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(_customer, CancellationToken.None));

            _writeRepoMock.Verify(p => p.DeleteItem(It.IsAny<CustomerWrite>()), Times.Never);
            Assert.Equal(string.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={_customer.Id}"),
            exception.Message);
        }

        [Fact]
        public async Task DeleteCustomerCommandHandler_GivenCorrectId_ResultAsTrue()
        {
            _writeRepoMock.Setup(p => p.Find(It.IsAny<Expression<Func<CustomerWrite, bool>>>()))
                .Returns(Task.FromResult<CustomerWrite?>(new CustomerWrite(
                                        "Mohammad",
                                        "taghy@gmail.com",
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        "+989384563280",
                                        "IR830120010000001387998021"
                                        )
                
            ));
            _writeRepoMock.Setup(p => p.DeleteItem(It.IsAny<CustomerWrite>()))
                .Returns(Task.FromResult<bool>(true));

            CommandResponse<bool> result = await _handler.Handle(_customer, CancellationToken.None);

            _writeRepoMock.Verify(p => p.DeleteItem(It.IsAny<CustomerWrite>()), Times.Once);
            Assert.True(result.Result);
        }

    }
}
