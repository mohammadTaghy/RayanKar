using Application.Test.Common;
using Application.UseCases.Customers.Command.Create;
using AutoMapper;
using Domain.WriteEntities;
using Shouldly;

namespace Application.Test.UseCases.Customers
{
    public class ProfileConfigure_Test : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;

        public ProfileConfigure_Test(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
       
        

    }
}
