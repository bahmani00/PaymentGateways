using LibraryApp.Data.Models;
using LibraryApp.Data.ViewModels;
using System.Collections.Generic;

namespace LibraryApp.Data.Services
{
    public interface IPaymentGateway
    {
        string GenerateToken(ViewModels.BookPurchaseVM product);

        PurchaseResult SubmitForSettlement(string itemId, string itemName, int userId, decimal price, string nonce);

        List<SubscriptionPlan> GetAllPlans();

        PurchaseResult SubscribeTo(string planId, string customerPaymentMethodToken);
        
        DashboardVM GetTotalBalance();
    }

    public interface IBraintreeService : IPaymentGateway
    {
    }

    public interface IStripeService : IPaymentGateway
    {
    }

    public interface IPaymentGatewayService : IPaymentGateway
    {
    }
}
