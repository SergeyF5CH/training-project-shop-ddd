using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingProjectShop.Domain.Customers;

namespace TrainingProjectShop.Application.Customers
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Guid> CreateCustomerAsync(string name, string email)
        {
            var customer = new Customer(
                Guid.NewGuid(), 
                name, 
                new Email(email));

            await _customerRepository.AddAsync(customer);

            return customer.Id;
        }
    }
}
