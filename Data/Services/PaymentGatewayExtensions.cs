﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace LibraryApp.Data.Services
{
    public static class PaymentGatewayExtensions
    {
        public static void AddPaymentGateway(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddBraintreeGateway(configuration);
            services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
        }

        #region Braintree
        public static void AddBraintreeGateway(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new Braintree.Configuration();
            var section = configuration.GetSection("BraintreeGateway");
            section.Bind(options);
            options.Environment = GetEnvironment(section.GetValue<string>("environment"));

            services.AddSingleton<IPaymentGateway>(c => 
            {
                var gatewayOptions = new Braintree.BraintreeGateway(options);

                return new BraintreeService(gatewayOptions);
            });
        }

        public static PurchaseResult ToPurchaseResult(this Braintree.Result<Braintree.Transaction> result)
        {
            return new PurchaseResult(result.IsSuccess());
        }
        public static PurchaseResult ToSubscription(this Braintree.Result<Braintree.Subscription> result)
        {
            return new PurchaseResult(result.IsSuccess());
        }

        private static Braintree.Environment GetEnvironment(string env)
        {
            return env == "PRODUCTION" ? Braintree.Environment.PRODUCTION : Braintree.Environment.SANDBOX; 
        }
        #endregion Braintree

    }
}
