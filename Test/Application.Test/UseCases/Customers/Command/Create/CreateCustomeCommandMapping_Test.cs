using Application.Test.Common;
using Application.UseCases.Customers.Command.Create;
using AutoMapper;
using Domain.WriteEntities;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test.UseCases.Customers.Command
{
    public class CreateCustomeCommandMapping_Test : IClassFixture<MappingTestsFixture>
    {
        private readonly IMapper _mapper;

        public CreateCustomeCommandMapping_Test(MappingTestsFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite_BankAccountValidation()
        {
            var entity = new CreateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+989384563280",
                "taghy@gmail.com",
                "6274121191332725"
            );

            Assert.Throws<AutoMapperMappingException>(() => _mapper.Map<CustomerWrite>(entity));
        }
        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite_PhoneNumberValidation()
        {
            var entity = new CreateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+9893845632802",
                "taghy@gmail.com",
                "IR830120010000001387998021"
            );

            Assert.Throws<AutoMapperMappingException>(() => _mapper.Map<CustomerWrite>(entity));
        }
        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite()
        {
            var entity = new CreateCustomerCommand(
                "Mohammad",
                "Yami",
                DateTime.Now.AddYears(-1),
                "+989384563280",
                "taghy@gmail.com",
                "IR830120010000001387998021"
            );

            var result = _mapper.Map<CustomerWrite>(entity);

            Assert.NotNull(result);
            result.ShouldBeOfType<CustomerWrite>();
        }


    }
}
