using System;

namespace PaymentGateways.Data.Models
{
    public class Product {
        public string Name { get; set; } 
        public string Description { get; set; } 
        public string UnitLabel { get; set; } 
        public string StatementDescriptor { get; set; } 
        public DateTime Created { get; set; } 

        public static explicit operator Product(Stripe.Product product){
            return new Product{
                Name = product.Name,
                Description = product.Description,
                UnitLabel = product.UnitLabel,
                StatementDescriptor = product.StatementDescriptor,
                Created = product.Created,
            };
        }             
    }
}