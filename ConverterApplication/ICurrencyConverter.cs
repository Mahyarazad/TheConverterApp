using System;
using System.Collections.Generic;

namespace ConverterApplication
{
    public interface ICurrencyConverter
    {
        void ClearConfiguration();
        void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);
        double Convert(string fromCurrency, string toCurrency, double amount);
    }
}