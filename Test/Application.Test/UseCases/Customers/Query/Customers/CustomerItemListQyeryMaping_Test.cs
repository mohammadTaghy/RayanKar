using Application.Test.Common;
using Application.UseCases.Customers.Query;
using AutoMapper;
using Domain.ReadEntitis;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Customers.Query.Customers
{
    public class CustomerItemListQyeryMaping_Test : IClassFixture<MappingTestsFixture>
    {
        private readonly IMapper _mapper;

        public CustomerItemListQyeryMaping_Test(MappingTestsFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite()
        {
            var entityList =
                new List<CustomerRead> {
                new CustomerRead(
                                        "Mohammad",
                                        new Domain.ValueObject.PhoneNumber("+989384563280"),
                                        "taghy@gmail.com",
                                        new Domain.ValueObject.BankAccountNumber("IR830120010000001387998021"),
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        1
                                    )
                };

            var result = _mapper.Map<List<CustomerDto>>(entityList);

            Assert.NotNull(result);
            result.ShouldBeOfType<List<CustomerDto>>();
        }


    }
}
