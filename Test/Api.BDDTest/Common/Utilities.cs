using Domain.WriteEntities;
using Newtonsoft.Json;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BDDTest.Common
{
    public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(PersistanceDBContext context)
        {
            context.Customer.Add(new CustomerWrite(
                                        "Mohammad",
                                        "taghy@gmail.com",
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        new Domain.ValueObject.PhoneNumber("+989384563280"),
                                        new Domain.ValueObject.BankAccountNumber("IR830120010000001387998021")
                                        )
                );

            context.SaveChanges();
        }
    }
}
