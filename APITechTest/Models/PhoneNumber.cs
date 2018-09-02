using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APITechTest.Models
{
    public class PhoneNumber
    {

        public Guid Id { get; set; }
        public string Number { get; set; }
        public Customer Customer { get; set; }
        public bool Active { get; set; }

    }
}
