using Qface.Domain.Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Qface.Domain.Shared.ValueObjects
{
    public class Money : ValueObjectType
    {
        public static Money Undefined => new Money(0, "GHS", Rate.Create(), "GHS");

        public decimal? Value { get; set; }
        public decimal? LocalValue { get; set; }
        public decimal? ForeignValue { get; set; }
        public string LocalCurrency { get; set; }
        public string ForeignCurrency { get; set; }
        public Rate Rate { get; set; }
        public string AmountText => $"{LocalCurrency} {Value}";

        Money() { } //For EF Core
        public static Money Of(decimal value, string currency, Rate rate, string foreignCurrency = null)
        {
            return new Money(value, currency, rate, foreignCurrency);
        }

        Money(decimal value, string currency, Rate rate, string foreignCurrency)
        {
            Value = value;
            LocalCurrency = currency;
            ForeignCurrency = foreignCurrency ?? currency;
            Rate = rate;
            LocalValue = Value;
            ForeignValue = Value * rate.Selling;
        }
    }
}
