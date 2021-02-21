using LibraryApp.Data.Models;
using System.Collections.Generic;

namespace LibraryApp.Data.Services
{
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly IPaymentGateway _PaymentGateway;

        public PaymentGatewayService(IPaymentGateway paymentGateway)
        {
            _PaymentGateway = paymentGateway;
        }

        public string GenerateToken(ViewModels.BookPurchaseVM product)
        {
            return _PaymentGateway.GenerateToken(product);
        }

        public PurchaseResult SubmitForSettlement(string itemId, string itemName, int userId, decimal price, string nonce)
        {
            return _PaymentGateway.SubmitForSettlement(itemId, itemName, userId, price, nonce);
        }

        public List<SubscriptionPlan> GetAllPlans()
        {
            return _PaymentGateway.GetAllPlans();
        }

        public PurchaseResult SubscribeTo(string planId, string customerPaymentMethodToken)
        {
            return _PaymentGateway.SubscribeTo(planId, customerPaymentMethodToken);
        }
    }
}
