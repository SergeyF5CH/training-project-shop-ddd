using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProjectShop.Domain.Customer
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

        //сделать методы блокировки пользователя, разблокировки, удаления, тесты
    }
}
