using Application.Test.Common;
using Application.UseCases.Customers.Command.Create;
using AutoMapper;
using Domain.WriteEntities;
using Shouldly;

namespace Application.Test.UseCases.Customers
{
    public class CustomerMapping_Test : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public CustomerMapping_Test(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite_BankAccountValidation()
        {
            var entity = new CreateCustomerCommand()
            {
                BankAccountNumber = "6274121191332725",
                DateOfBirth = DateTime.Parse("1/10/2022"),
                Email = "m.taghy.yami@gmail.com",
                Firstname = "Mohammad",
                LastName = "Yami",
                PhoneNumber = "+989384563280"
            };

            Assert.Throws<AutoMapperMappingException>(()=>_mapper.Map<CustomerWrite>(entity));
        }
        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite_PhoneNumberValidation()
        {
            var entity = new CreateCustomerCommand()
            {
                BankAccountNumber = "IR830120010000001387998021",
                DateOfBirth = DateTime.Parse("1/10/2022"),
                Email = "m.taghy.yami@gmail.com",
                Firstname = "Mohammad",
                LastName = "Yami",
                PhoneNumber = "+9893845632802"
            };

            Assert.Throws<AutoMapperMappingException>(() => _mapper.Map<CustomerWrite>(entity));
        }
        [Fact]
        public void ShouldMap_CreateCustomerCommand_CustomerErite()
        {
            var entity = new CreateCustomerCommand()
            {
                BankAccountNumber = "IR830120010000001387998021",
                DateOfBirth = DateTime.Parse("1/10/2022"),
                Email = "m.taghy.yami@gmail.com",
                Firstname = "Mohammad",
                LastName = "Yami",
                PhoneNumber = "+989384563280"
            };

            var result = _mapper.Map<CustomerWrite>(entity);

            Assert.NotNull(result);
            result.ShouldBeOfType<CustomerWrite>();
        }
        

    }
}
