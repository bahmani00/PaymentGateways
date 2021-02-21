using Braintree;
using System;
using System.Collections.Generic;

namespace LibraryApp.Data.Services
{
    public class BraintreeService : IBraintreeService
    {
        private readonly IBraintreeGateway braintreeGateway;

        public BraintreeService(IBraintreeGateway braintreeGateway)
        {
            this.braintreeGateway = braintreeGateway;
        }

        public string GenerateToken()
        {
            return braintreeGateway.ClientToken.Generate();
        }

        public Result<Transaction> SubmitForSettlement(Guid itemId, int userId, decimal price, string nonce)
        {
            //var submitForSettlementRequest = new TransactionRequest {
            //    PurchaseOrderNumber = "12345",
            //    TaxAmount = 5.00M,
            //    ShippingAmount = 1.00M,
            //    DiscountAmount = 0.00M,
            //    ShipsFromPostalCode = "60654",
            //    LineItems = new TransactionLineItemRequest[] {
            //        new TransactionLineItemRequest
            //        {
            //            Name = "Product",
            //            LineItemKind = TransactionLineItemKind.DEBIT,
            //            Quantity = 10.0000M,
            //            UnitAmount = 9.5000M,
            //            UnitOfMeasure = "unit",
            //            TotalAmount = 95.00M,
            //            TaxAmount = 5.00M,
            //            DiscountAmount = 0.00M,
            //            ProductCode = "54321",
            //            CommodityCode = "98765"
            //        }
            //    }
            //};

            var request = new TransactionRequest {
                //CustomerId = userId.ToString(),
                Amount = price,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest {
                    SubmitForSettlement = true
                }
            };

            return braintreeGateway.Transaction.Sale(request);
        }

        public List<Plan> GetAllPlans()
        {
            return braintreeGateway.Plan.All();
        }

        public Result<Subscription> SubscribeTo(string id, string customerPaymentMethodToken)
        {
            var subscriptionRequest = new SubscriptionRequest() {
                PaymentMethodToken = customerPaymentMethodToken,
                PlanId = id
            };

            return braintreeGateway.Subscription.Create(subscriptionRequest);
        }
    }
}
