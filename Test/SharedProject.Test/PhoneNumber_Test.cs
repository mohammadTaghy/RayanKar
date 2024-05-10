using SharedProject.Validation;

namespace SharedProject.Test
{
    public class PhoneNumber_Test
    {
        [Theory]
        [InlineData("+9893845632801", false)]
        [InlineData("+989384563280", true)]
        [InlineData("+982133367289", true)]
        public void PhoneNumber_Validation_Test(string number, bool expected)
        {

            bool result = PhoneNumber.Validate(number);

            Assert.Equal(expected, result);
        }
        
        
    }
}
