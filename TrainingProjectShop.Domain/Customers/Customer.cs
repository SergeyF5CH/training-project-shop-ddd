using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProjectShop.Domain.Customers
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public CustomerStatus Status { get; private set; }

        public Customer(Guid id, 
            string name, 
            Email email)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Customer id can't be empty");
            }

            if (string.IsNullOrWhiteSpace(name)) 
            { 
                throw new ArgumentException("Customer name can't be empty"); 
            }

            Id = id; 
            Name = name; 
            Email = email; 
            Status = CustomerStatus.Active;
        }

        public void Block()
        {
            if (Status != CustomerStatus.Active)
            {
                throw new InvalidOperationException("Only Active customers can be blocked");
            }

            Status = CustomerStatus.Blocked;
        }

        public void Unblocked()
        {
            if (Status != CustomerStatus.Blocked)
            {
                throw new InvalidOperationException("Only Blocked customers can be unblocked");
            }

            Status = CustomerStatus.Active;
        }

        public void Delete()
        {
            if (Status == CustomerStatus.Deleted)
            {
                throw new InvalidOperationException("Customer is already deleted");
            }

            Status = CustomerStatus.Deleted;
        }
    }
}
