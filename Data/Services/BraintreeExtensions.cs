using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApp.Data.Services
{
    public static class BraintreeExtensions
    {
        public static void AddBraintreeGateway(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new Braintree.Configuration();
            var section = configuration.GetSection("braintreeGateway");
            section.Bind(options);
            options.Environment = GetEnvironment(section.GetValue<string>("environment"));

            services.AddSingleton<IBraintreeService>(c => 
            {
                var gatewayOptions = new Braintree.BraintreeGateway(options);

                return new BraintreeService(gatewayOptions);
            });
        }

        private static Braintree.Environment GetEnvironment(string env)
        {
            return env == "PRODUCTION" ? Braintree.Environment.PRODUCTION : Braintree.Environment.SANDBOX; 
        }
    }
}
