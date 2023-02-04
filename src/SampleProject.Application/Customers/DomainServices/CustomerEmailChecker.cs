using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using SampleProject.Domain.Customers;

namespace SampleProject.Application.Customers.DomainServices
{
    class CustomerEmailChecker : ICustomerEmailChecker
    {
        private readonly string _customerEmail;

        public CustomerEmailChecker(string customerEmail)
        {
            _customerEmail = customerEmail;
        }

        public bool IsValid(string customerEmail)
        {
            try
            {
                var emailAddress = new MailAddress(customerEmail);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
