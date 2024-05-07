using Application.Test.Common;
using Application.UseCases.Customers.Command.Uodate;
using AutoMapper;
using Domain.WriteEntities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Customers.Command.Update
{
    public class UpdateCustomeCommandMapping_Test : IClassFixture<MappingTestsFixture>
    {
        private readonly IMapper _mapper;

        public UpdateCustomeCommandMapping_Test(MappingTestsFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite()
        {
            var entity = new UpdateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+989384563280",
                "taghy@gmail.com",
                "IR830120010000001387998021",
                1
            );

            var result = _mapper.Map<CustomerWrite>(entity);

            Assert.NotNull(result);
            result.ShouldBeOfType<CustomerWrite>();
        }


    }
}
