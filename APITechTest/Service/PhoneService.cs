using APITechTest.Interfaces;
using APITechTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITechTest.Service
{
    public class PhoneService : IPhoneService
    {
        private List<PhoneNumber> PhoneNumbers { get; set; }
        private List<Customer> Customers { get; set; }

        public PhoneService()
        {
            //Generate some random data for testing
            Customers = new List<Customer>();
            PhoneNumbers = new List<PhoneNumber>();
            for (int i = 0; i < 3; i++)
            {
                Customers.Add(new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = String.Format("Customer{0}", i)
                });
            }
            Random rnd = new Random();
            for (int i = 0; i < 30; i++)
            {
                PhoneNumbers.Add(new PhoneNumber
                {
                    Id = Guid.NewGuid(),
                    Number = String.Format("0{0}{1}", rnd.Next(10000, 99999), rnd.Next(10000, 99999)),
                    Customer = Customers[rnd.Next(0, 2)],
                    Active = false
                });
            }
        }
    

        public Customer FindCustomerByGuid(Guid guid)
        {
            Customer customer = Customers.FirstOrDefault(c => c.Id == guid);
            if (customer == default(Customer))
                customer = null;
            return customer;
        }

        public Customer FindCustomerByName(string name)
        {
            Customer customer = Customers.FirstOrDefault(c => c.Name == name);
            if (customer == default(Customer))
                customer = null;
            return customer;
        }

        public List<Customer> GetAllCustomers()
        {
            return Customers;
        }

        public List<PhoneNumber> GetAllPhoneNumbers()
        {
            return PhoneNumbers;
        }

        public List<PhoneNumber> GetCustomerPhoneNumbers(Customer customer)
        {
            return PhoneNumbers.Where(p => p.Customer == customer).ToList<PhoneNumber>();
        }

        public bool TestAPI()
        {
            return true;
        }


    }
}
