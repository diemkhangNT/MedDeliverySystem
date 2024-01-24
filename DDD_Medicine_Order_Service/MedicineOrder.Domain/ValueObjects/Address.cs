using MedicineOrder.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.ValueObjects
{
    public class Address
    {
        private Address() { }

        public Address(string address1,
                       string address2,
                       string suburb,
                       string state,
                       string postCode,
                       int pharmacyId)
        {
            Validate(address1, address2, suburb, state, postCode);
            Address1 = address1;
            Address2 = address2;
            Suburb = suburb;
            State = state;
            PostCode = postCode;
            PharmacyId = pharmacyId;
        }

        private void Validate(string address1,
                              string address2,
                              string suburb,
                              string state,
                              string postCode)
        {
            if (!string.IsNullOrEmpty(address1) && address1.Length > 50)
                throw new InvalidArgumentException(nameof(Address1), "cannot contain more than 50 letters");

            if (!string.IsNullOrEmpty(address2) && address2.Length > 50)
                throw new InvalidArgumentException(nameof(Address2), "cannot contain more than 50 letters");

            if (!string.IsNullOrEmpty(suburb) && suburb.Length > 50)
                throw new InvalidArgumentException(nameof(Suburb), "cannot contain more than 50 letters");

            if (!string.IsNullOrEmpty(state) && state.Length > 10)
                throw new InvalidArgumentException(nameof(State), "cannot contain more than 10 letters");

            if (!string.IsNullOrEmpty(postCode) && postCode.Length > 10)
                throw new InvalidArgumentException(nameof(PostCode), "cannot contain more than 10 letters");
        }

        public string Address1 { get; private set; }

        public string Address2 { get; private set; }

        public string Suburb { get; private set; }

        public string State { get; private set; }

        public string PostCode { get; private set; }
        
        public int PharmacyId { get; private set; }

    }
}
