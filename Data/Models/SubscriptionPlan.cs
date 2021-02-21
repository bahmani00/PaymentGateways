using System;

namespace LibraryApp.Data.Models
{
    public class SubscriptionPlan
    {
        public int? BillingDayOfMonth { get; private set; }
        public int? BillingFrequency { get; private set; }
        public string CurrencyIsoCode { get; private set; }
        public string Description { get; private set; }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int? NumberOfBillingCycles { get; private set; }
        public decimal? Price { get; private set; }
        public bool? TrialPeriod { get; private set; }
        public int? TrialDuration { get; private set; }

        public static explicit operator SubscriptionPlan(Braintree.Plan plan)
        {
            return new SubscriptionPlan {
                Id = plan.Id,
                BillingDayOfMonth = plan.BillingDayOfMonth,
                BillingFrequency = plan.BillingFrequency,
                CurrencyIsoCode = plan.CurrencyIsoCode,
                Description = plan.Description,
                Name = plan.Name,
                NumberOfBillingCycles = plan.NumberOfBillingCycles,
                Price = plan.Price,
                TrialPeriod = plan.TrialPeriod,
                TrialDuration = plan.TrialDuration,
            };
        }

        public static explicit operator SubscriptionPlan(Stripe.Plan plan)
        {
            return new SubscriptionPlan {
                Id = plan.Id,
                BillingFrequency = (int)plan.IntervalCount,
                CurrencyIsoCode = plan.Currency,
                Description = plan.Nickname,
                Name = plan.Nickname,
                Price = Convert.ToDecimal(plan.AmountDecimal) / 100,
                TrialPeriod = plan.TrialPeriodDays.HasValue,
                TrialDuration = (int?)plan.TrialPeriodDays,
            };
        }
    }
}
