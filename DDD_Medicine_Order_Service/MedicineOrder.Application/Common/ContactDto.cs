using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Common
{
    public class ContactDto
    {
        public ContactDto(string email, string phoneNumber, int pharmacyId)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            PharmacyId = pharmacyId;
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PharmacyId { get; set; }
    }
}
