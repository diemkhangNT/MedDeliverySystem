using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Application.Common
{
    public class AddressDto
    {
        public AddressDto(string address1, string address2, string suburb, string state, string postCode, int pharmacyId)
        {
            Address1 = address1;
            Address2 = address2;
            Suburb = suburb;
            State = state;
            PostCode = postCode;
            PharmacyId = pharmacyId;
        }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Suburb { get; set; }

        public string State { get; set; }

        public string PostCode { get; set; }

        public int PharmacyId { get; set; }
    }
}
