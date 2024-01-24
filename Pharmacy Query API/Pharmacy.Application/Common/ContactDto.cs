using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Application.Common
{
    public class ContactDto
    {
        public ContactDto(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
