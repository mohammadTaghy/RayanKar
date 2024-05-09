using SharedProject.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedProject.Test
{
    public class CustomerValidate_Test
    {
        private CustomerDto _customer;

        public CustomerValidate_Test()
        {
            _customer = new CustomerDto(0, "Mohammad", "Yami", DateTime.Now, "taghy@gmail.com", 
                "+989384563280", "IR830120010000001387998021");
        }
        [Fact]
        public void CommonValidate_PhoneNumberNotValid_FalseResult()
        {
            _customer.PhoneNumber = "+9893845632801";

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.False(result);
        }
        [Fact]
        public void CommonValidate_BankAccountNotValid_FalseResult()
        {
            _customer.BankAccountNumber = "IR830120010000001";

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.False(result);
        }
        [Fact]
        public void CommonValidate_EmailNotValid_FalseResult()
        {
            _customer.Email = "dssda";

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.False(result);
        }
        [Fact]
        public void CommonValidate_FirstnameNotValid_FalseResult()
        {
            _customer.Firstname = "";

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.False(result);
        }
        [Fact]
        public void CommonValidate_LastNameNotValid_FalseResult()
        {
            _customer.LastName = "";

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.False(result);
        }
        [Fact]
        public void CommonValidate_ValidCustomer_TrueResult()
        {

            bool result = CustomerValidate.CommonValidate(_customer, out _);

            Assert.True(result);
        }
    }
}
