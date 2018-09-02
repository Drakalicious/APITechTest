using APITechTest.Controllers;
using APITechTest.Interfaces;
using APITechTest.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestAPITech
{
    public class PhoneNumerControllerTest
    {

        private Mock<IPhoneService> serviceMock;
        private PhoneNumberController controller;

        public PhoneNumerControllerTest()
        {
            serviceMock = new Mock<IPhoneService>();
            controller = new PhoneNumberController(serviceMock.Object);
        }

        [Fact]
        public void Get_APITest()
        {
            var result = controller.TestAPI();
            Assert.Equal(true, result);
        }
        
        [Fact]
        public void Get_AllPhoneNumbers_ResultsNotFound()
        {
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(new List<PhoneNumber>());
            var results = controller.AllPhoneNumbers();
            List<PhoneNumber> list = Assert.IsType<List<PhoneNumber>>(results);

            Assert.Equal(0, list.Count());
        }

        [Fact]
        public void Get_AllPhoneNumbers_CountCorrect()
        {
            Customer customer1 = new Customer(){Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"),Name = "Customer 1"};
            Customer customer2 = new Customer(){Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"),Name = "Customer 2"};

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("3d8cedf1-e3f7-4ae5-b357-a3c80b01c5b8"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f47a84d6-3299-4e84-b7d3-38cbdf3eb90d"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("b6057cf8-4309-4e0c-83f3-031e28ec4df7"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("48ffc056-2e24-4168-b907-5c95e423f842"),Number = "02012345675",Customer = customer2,Active = false}
            };

            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.AllPhoneNumbers();
            List<PhoneNumber> list = Assert.IsType<List<PhoneNumber>>(results);

            Assert.Equal(4, list.Count());
           }

        [Fact]
        public void Get_AllPhoneNumbers_ValuesCorrect()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer 1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer 2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.AllPhoneNumbers();
            List<PhoneNumber> list = Assert.IsType<List<PhoneNumber>>(results);

            Assert.Equal("02012345676", list[2].Number);
        }
        
        [Fact]
        public void Get_CustomerPhoneNumbers_ResultsNotFound()
        {           
            var results = controller.CustomerPhoneNumbers(new Guid("5d9aa565-3b91-418c-a39a-a017d87e48fd"));

            var viewResult = Assert.IsType<NotFoundResult>(results);
        }

        [Fact]
        public void Get_CustomerPhoneNumbers_CountCorrect()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer2" };
            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("3d8cedf1-e3f7-4ae5-b357-a3c80b01c5b8"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f47a84d6-3299-4e84-b7d3-38cbdf3eb90d"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("b6057cf8-4309-4e0c-83f3-031e28ec4df7"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("48ffc056-2e24-4168-b907-5c95e423f842"),Number = "02012345675",Customer = customer2,Active = false}
            };
            var mockCustomers = new List<Customer>
            {
                customer1,
                customer2
            };
            serviceMock.Setup(service => service.GetAllCustomers()).Returns(mockCustomers);
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.CustomerPhoneNumbers(new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"));
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<PhoneNumber> list = Assert.IsAssignableFrom<List<PhoneNumber>>(okResult.Value);

            Assert.Equal(3, list.Count());
        }

        [Fact]
        public void Get_CustomerPhoneNumbers_ValuesCorrect()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer2" };
            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("3d8cedf1-e3f7-4ae5-b357-a3c80b01c5b8"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f47a84d6-3299-4e84-b7d3-38cbdf3eb90d"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("b6057cf8-4309-4e0c-83f3-031e28ec4df7"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("48ffc056-2e24-4168-b907-5c95e423f842"),Number = "02012345675",Customer = customer2,Active = false}
            };
            var mockCustomers = new List<Customer>
            {
                customer1,
                customer2
            };
            serviceMock.Setup(service => service.GetAllCustomers()).Returns(mockCustomers);
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.CustomerPhoneNumbers(new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"));
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<PhoneNumber> list = Assert.IsAssignableFrom<List<PhoneNumber>>(okResult.Value);


            Assert.Equal("02012345676", list[2].Number);
        }

        [Fact]
        public void Get_CustomerPhoneNumbersName_ResultsNotFound()
        {
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(new List<PhoneNumber>());
            var results = controller.AllPhoneNumbers();
            List<PhoneNumber> list = Assert.IsType<List<PhoneNumber>>(results);

            Assert.Equal(0, list.Count());
        }

        [Fact]
        public void Get_CustomerPhoneNumbersName_CountCorrect()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("3d8cedf1-e3f7-4ae5-b357-a3c80b01c5b8"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f47a84d6-3299-4e84-b7d3-38cbdf3eb90d"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("b6057cf8-4309-4e0c-83f3-031e28ec4df7"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("48ffc056-2e24-4168-b907-5c95e423f842"),Number = "02012345675",Customer = customer2,Active = false}
            };
            var mockCustomers = new List<Customer>
            {
                customer1,
                customer2
            };
            serviceMock.Setup(service => service.GetAllCustomers()).Returns(mockCustomers);
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.CustomerPhoneNumbersName("Customer1");
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<PhoneNumber> list = Assert.IsAssignableFrom<List<PhoneNumber>>(okResult.Value);

            Assert.Equal(3, list.Count());
        }

        [Fact]
        public void Get_CustomerPhoneNumbersName_ValuesCorrect()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            var mockCustomers = new List<Customer>
            {
                customer1,
                customer2
            };
            serviceMock.Setup(service => service.GetAllCustomers()).Returns(mockCustomers);
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.CustomerPhoneNumbersName("Customer1");
            OkObjectResult okResult = Assert.IsType<OkObjectResult>(results);
            List<PhoneNumber> list = Assert.IsAssignableFrom<List<PhoneNumber>>(okResult.Value);

            Assert.Equal("02012345676", list[2].Number);
        }

        [Fact]
        public void Post_Activate_Number_OK()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer 1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer 2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.Activate("02012345678");

            Assert.IsType<OkResult>(results);
        }

        [Fact]
        public void Post_Activate_Number_NotFound()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer 1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer 2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.Activate("02012345999");

            Assert.IsType<NotFoundResult>(results);
        }

        [Fact]
        public void Post_Activate_Guid_OK()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer 1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer 2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.ActivateByGuid(new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"));

            Assert.IsType<OkResult>(results);
        }

        [Fact]
        public void Post_Activate_Guid_NotFound()
        {
            Customer customer1 = new Customer() { Id = new Guid("dbe6f726-51b3-4c7b-ab6c-af38e8ff1bb3"), Name = "Customer 1" };
            Customer customer2 = new Customer() { Id = new Guid("9d1bc981-e351-4496-851a-2947d1872a89"), Name = "Customer 2" };

            var mockPhoneNumbers = new List<PhoneNumber>
            {
                new PhoneNumber(){Id = new Guid("a8515bba-1b68-44e3-a108-ebfbc3f478b4"),Number = "02012345678",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("17082294-a7b6-4222-bfc8-f128491ef420"),Number = "02012345677",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("f1cd0a56-a4bf-4fb7-8417-607296f4589e"),Number = "02012345676",Customer = customer1,Active = false},
                new PhoneNumber(){Id = new Guid("fcf9250b-a066-4728-866a-e84a5c519d1a"),Number = "02012345675",Customer = customer2,Active = false}
            };
            serviceMock.Setup(service => service.GetAllPhoneNumbers()).Returns(mockPhoneNumbers);

            var results = controller.ActivateByGuid(new Guid("75699e0b-aeee-46ea-80de-a5b8890bfebc"));

            Assert.IsType<NotFoundResult>(results);
        }

    }


}
