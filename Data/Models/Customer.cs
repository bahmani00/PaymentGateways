using System;

namespace PaymentGateways.Data.Models
{
    public class Customer {
        public string Name { get; set; } 
        public long Balance { get; set; }
        public DateTime Created { get; set; } 

        public static explicit operator Customer(Stripe.Customer customer){
            return new Customer{
                Name = customer.Name,
                Balance = customer.Balance,
                Created = customer.Created,
            };
        }             
    }
}