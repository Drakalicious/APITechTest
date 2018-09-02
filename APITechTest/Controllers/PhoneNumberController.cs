using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APITechTest.Service;
using APITechTest.Interfaces;
using APITechTest.Models;

namespace APITechTest.Controllers
{
    [Produces("application/json")]
    public class PhoneNumberController : Controller
    {
        private IPhoneService PhoneService;

        public PhoneNumberController(IPhoneService service)
        {
            PhoneService = service;
        }


        /*
         * API description:
         * Test API
         * --
         * API endpoint:
         * GET example.org/api/TestAPI
         * --
         * API implementation:
         */
        [HttpGet("api/TestAPI")]
        public bool TestAPI()
        {
            return true;
        }

        /*
        * API description:
        * GET All Phone Numbers
        * --
        * API endpoint:
        * GET example.org/api/AllPhoneNumbers
        * --
        * API implementation:
        */
        [HttpGet("api/AllPhoneNumbers")]
        public IEnumerable<PhoneNumber> AllPhoneNumbers()
        {
            List<PhoneNumber> phoneNumbers = PhoneService.GetAllPhoneNumbers();
            return phoneNumbers;
        }

        /*
        * API description:
        * GET All Phone Numbers For a Customer
        * --
        * API endpoint:
        * GET example.org/api/CustomerPhoneNumbers/f32b8c66-6824-422a-b5db-f2defcbfceba
        * --
        * API implementation:
        */
        [HttpGet("api/CustomerPhoneNumbers/{guid}")]
        public IActionResult CustomerPhoneNumbers(Guid guid)
        {
            Customer customer;
            try
            {
                //customer = PhoneService.FindCustomerByGuid(guid);
                //Moved to controller to enable testing with Moq
                List<Customer> customers = PhoneService.GetAllCustomers();
                customer = customers.FirstOrDefault(c => c.Id == guid);
                if (customer == default(Customer))
                    customer = null;
            }
            catch
            {
                return NotFound();
            }

            if (customer == null)
                return NotFound();
            List<PhoneNumber> phoneNumbers = PhoneService.GetAllPhoneNumbers();
            var results = phoneNumbers.Where(p => p.Customer == customer).ToList<PhoneNumber>();
            return Ok(results);
        }

        /*
        * API description:
        * GET All Phone Numbers For a Customer
        * --
        * API endpoint:
        * GET example.org/api/CustomerPhoneNumbersName/CustomerName
        * --
        * API implementation:
        */
        [HttpGet("api/CustomerPhoneNumbersName/{name}")]
        public IActionResult CustomerPhoneNumbersName(string name)
        {
            Customer customer;
            try
            {
                //customer = PhoneService.FindCustomerByName(name);
                //Moved to controller to enable testing with Moq
                List<Customer> customers = PhoneService.GetAllCustomers();
                customer = customers.FirstOrDefault(c => c.Name == name);
                if (customer == default(Customer))
                    customer = null;
            }
            catch
            {
                return NotFound();
            }

            if (customer == null)
                return NotFound();
            List<PhoneNumber> phoneNumbers = PhoneService.GetAllPhoneNumbers();
            var results = phoneNumbers.Where(p => p.Customer == customer).ToList<PhoneNumber>();
            return Ok(results);
        }

        /*
        * API description:
        * POST Activate a Phone Number
        * --
        * API endpoint:
        * POST example.org/api/Activate/02012345678
        * --
        * API implementation:
        */
        [HttpGet("api/Activate/{number}")]
        public IActionResult Activate(string number)
        {
            try
            {
                List<PhoneNumber> phoneNumbers = PhoneService.GetAllPhoneNumbers();
                PhoneNumber phoneNumber = phoneNumbers.FirstOrDefault(n => n.Number == number);
                if (phoneNumber == default(PhoneNumber))
                    return NotFound();
                phoneNumber.Active = true;
                //Update Context Here
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }


        /*
        * API description:
        * POST Activate a Phone Number
        * --
        * API endpoint:
        * POST example.org/api/Activate/f92283b5-193f-4a7f-8958-a0e7ed1d7ed5
        * --
        * API implementation:
        */
        [HttpGet("api/ActivateGuid/{name}")]
        public IActionResult ActivateByGuid(Guid guid)
        {
            try
            {
                List<PhoneNumber> phoneNumbers = PhoneService.GetAllPhoneNumbers();
                PhoneNumber number = phoneNumbers.FirstOrDefault(n => n.Id == guid);
                if (number == default(PhoneNumber))
                    return NotFound();
                number.Active = true;
                //Update Context Here
            }
            catch
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
