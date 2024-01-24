using Pharmacy.Domain.DomainCommands;
using Pharmacy.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pharmacy.Domain.ValueObjects
{
    public class Contact
    {
        private Contact()
        {
        }

        public Contact(string numberPhone, string email, int pharmacyId)
        {
            Validate(email, numberPhone);
            NumberPhone = numberPhone;
            Email = email;
            PharmacyId = pharmacyId;
        }

        public void Validate(string email, string numberPhone)
        {
            string patternNumberPhone = @"^(0|\+84)(\d{9,10})$";
            bool isNumber = Regex.IsMatch(numberPhone, "^[0-9]+$")
                ? (Regex.IsMatch(numberPhone, patternNumberPhone)
                    ? true : throw new InvalidArgumentException(nameof(NumberPhone), "cannot contain more than 10 numbers!"))
                    : throw new InvalidArgumentException(nameof(NumberPhone), "cannot contain any letter!");
            
            string patternEmail = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
            bool isEmail = Regex.IsMatch(email, patternEmail)
                ? true : throw new InvalidArgumentException(nameof(Email), "is not valid!");
        }

        public string NumberPhone { get; private set; }
        public string Email { get; private set; }
        public int PharmacyId { get; private set; }
    }
}
