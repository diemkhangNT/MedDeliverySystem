using MedicineOrder.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MedicineOrder.Domain.ValueObjects
{
    public class Contact
    {
        public Contact()
        {
        }

        public Contact(string email, string numberPhone, int pharmacyId)
        {
            Validate(email, numberPhone);
            Email = email;
            NumberPhone = numberPhone;
            PharmacyId = pharmacyId;
        }
        public void Validate(string email, string numberPhone)
        {
            //string EmailPattern = @"^\s*[\w\-\+_']+(\.[\w\-\+_']+)*\@[A-Za-z0-9]([\w\.-]*[A-Za-z0-9])?\.[A-Za-z][A-Za-z\.]*[A-Za-z]$";
            //bool flag = Regex.IsMatch(EmailPattern, email);
            //if (!string.IsNullOrEmpty(email) && flag == false)
            //{
            //    throw new InvalidArgumentException(nameof(email), "Email is invalid!");
            //}
            if(!string.IsNullOrEmpty(numberPhone) && numberPhone.Length > 10)
            {
                throw new InvalidArgumentException(nameof(numberPhone), "cannot contain more than 10 numbers!");
            }
        }
        public string Email { get; private set; }
        public string NumberPhone { get; private set; }
        public int PharmacyId { get; private set; }
    }
}
