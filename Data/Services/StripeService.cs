using LibraryApp.Data.Models;
using LibraryApp.Data.ViewModels;
using Stripe;
using Stripe.Checkout;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp.Data.Services
{
    public class StripeService : IStripeService
    {
        //https://stripe.com/docs/payments/checkout/migration#api-products-after
        public string GenerateToken(ViewModels.BookPurchaseVM product)
        {
            var options = new SessionCreateOptions {
                PaymentMethodTypes = new List<string> {
                    "card",
                    },
                    LineItems = new List<SessionLineItemOptions> {
                    new SessionLineItemOptions {
                        PriceData = new SessionLineItemPriceDataOptions {
                            Product = product.Id.ToString(),
                            UnitAmount = (long)(product.Price * 100),
                            Currency = "cad",
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:44376/checkout/success",
                CancelUrl = "https://localhost:44376/checkout/failure",
            };

            var service = new SessionService();
            Session session = service.Create(options);
            return session.Id;
        }

        public PurchaseResult SubmitForSettlement(string itemId, string itemName, int userId, decimal price, string nonce)
        {
            var chargeOptions = new ChargeCreateOptions() {
                Amount = (long)(price * 100),
                Currency = "cad",
                Source = nonce, //stripeToken,
                Metadata = new Dictionary<string, string>()
                {
                    {"BookId", itemId },
                    {"BookTitle", itemName },
                    {"CustomerId", userId.ToString() },
                }
            };

            var service = new ChargeService();

            return service.Create(chargeOptions)
            .ToPurchaseResult();
        }

        public List<SubscriptionPlan> GetAllPlans()
        {
            var service = new PlanService();
            return service.List()
                .Select(x => (SubscriptionPlan)x).ToList();
        }

        public PurchaseResult SubscribeTo(string planId, string customerPaymentMethodToken)
        {
            var subscriptionOptions = new SubscriptionCreateOptions {
                Customer = customerPaymentMethodToken,
                Items = new List<SubscriptionItemOptions> {
                    new SubscriptionItemOptions {
                        Plan = planId
                    }
                }
            };

            var service = new SubscriptionService();
            return service.Create(subscriptionOptions)
                .ToSubscription();
        }        

        public TransactionBundle GetAllTransactions()
        {
            var response = new TransactionBundle();

            var balanceService = new BalanceService();
            response.Balance = (PaymentGateways.Data.Models.Balance)balanceService.Get();

            var transactionService = new BalanceTransactionService();
            response.Transactions = transactionService.List()
                    .Select(x => (PaymentGateways.Data.Models.BalanceTransaction)x).ToList();

            var customerService = new CustomerService();
            response.Customers = customerService.List()
                    .Select(x => (PaymentGateways.Data.Models.Customer)x).ToList();

            var chargeService = new ChargeService();
            response.Charges = chargeService.List()
                    .Select(x => (PaymentGateways.Data.Models.Charge)x).ToList();

            return response;        
        }
    }
}
