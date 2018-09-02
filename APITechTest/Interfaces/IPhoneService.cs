using APITechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITechTest.Interfaces
{
    public interface IPhoneService
    {

        bool TestAPI();
        Customer FindCustomerByGuid(Guid guid);
        Customer FindCustomerByName(string name);

        List<PhoneNumber> GetAllPhoneNumbers();
        List<Customer> GetAllCustomers();
        List<PhoneNumber> GetCustomerPhoneNumbers(Customer customer);
        //Internal service method for testing with moq
        
    }
}
