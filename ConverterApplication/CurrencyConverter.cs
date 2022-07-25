using System;
using System.Collections.Generic;
using System.Linq;

namespace ConverterApplication
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private List<Tuple<string, string, double>> ConversationTable =
            new List<Tuple<string, string, double>>
            {
                new ("USD","CAD",1.29),
                new ("CAD","GBP",0.64),
                new ("USD","EUR",0.98)
            };

        public void ClearConfiguration()
        {
            if (ConversationTable.Count > 3)
                ConversationTable.RemoveAt(ConversationTable.Count - 1);
        }



        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            if (conversionRates.First().Item1 == "USD" && conversionRates.First().Item2 == "GBP")
            {
                ConversationTable.Add(
                        new("USD", "GBP",
                        ConversationTable.FirstOrDefault(x => x.Item1 == "USD").Item3
                        * ConversationTable.FirstOrDefault(x => x.Item2 == "GBP").Item3));
            }

            if (conversionRates.First().Item1 == "GBP" && conversionRates.First().Item2 == "USD")
            {
                ConversationTable.Add(
                        new("GBP", "USD",
                        1 / ConversationTable.FirstOrDefault(x => x.Item1 == "USD").Item3
                        * 1 / ConversationTable.FirstOrDefault(x => x.Item2 == "GBP").Item3));
            }

            if (conversionRates.First().Item1 == "CAD" && conversionRates.First().Item2 == "EUR")
            {
                ConversationTable.Add(
                        new("CAD", "EUR",
                        1 / ConversationTable.FirstOrDefault(x => x.Item1 == "USD").Item3
                        * ConversationTable.FirstOrDefault(x => x.Item2 == "EUR").Item3));
            }

            if (conversionRates.First().Item1 == "EUR" && conversionRates.First().Item2 == "CAD")
            {
                ConversationTable.Add(
                        new("EUR", "CAD",
                        ConversationTable.FirstOrDefault(x => x.Item1 == "USD").Item3
                        * 1 / ConversationTable.FirstOrDefault(x => x.Item2 == "EUR").Item3));
            }

            if (conversionRates.First().Item1 == "GBP" && conversionRates.First().Item2 == "CAD")
            {
                ConversationTable.Add(
                        new("GBP", "CAD",
                        1 / ConversationTable.FirstOrDefault(x => x.Item2 == "GBP").Item3));
            }

            if (conversionRates.First().Item1 == "EUR" && conversionRates.First().Item2 == "USD")
            {
                ConversationTable.Add(
                        new("EUR", "USD",
                        1 / ConversationTable.FirstOrDefault(x => x.Item2 == "EUR").Item3));
            }

            if (conversionRates.First().Item1 == "CAD" && conversionRates.First().Item2 == "USD")
            {
                ConversationTable.Add(
                        new("CAD", "USD",
                        1 / ConversationTable.FirstOrDefault(x => x.Item2 == "CAD").Item3));
            }

            if (conversionRates.First().Item1 == "EUR" && conversionRates.First().Item2 == "GBP")
            {
                ConversationTable.Add(
                        new("EUR", "GBP",
                        1 / ConversationTable.FirstOrDefault(x => x.Item2 == "EUR").Item3
                        * ConversationTable.FirstOrDefault(x => x.Item2 == "CAD").Item3
                        * ConversationTable.FirstOrDefault(x => x.Item2 == "GBP").Item3));
            }

            if (conversionRates.First().Item1 == "GBP" && conversionRates.First().Item2 == "EUR")
            {
                ConversationTable.Add(
                        new("GBP", "EUR",
                        ConversationTable.FirstOrDefault(x => x.Item2 == "EUR").Item3
                        * 1 / ConversationTable.FirstOrDefault(x => x.Item2 == "CAD").Item3
                        * 1 / ConversationTable.FirstOrDefault(x => x.Item2 == "GBP").Item3));
            }
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            ClearConfiguration();
            if (fromCurrency == toCurrency)
                return amount;

            Tuple<string, string, double> first =
                ConversationTable
                    .FirstOrDefault(x =>
                        x.Item1 == fromCurrency && x.Item2 == toCurrency);

            if (first != null)
            {
                return amount * first.Item3;
            }

            if (ConversationTable
                    .Any(x => x.Item1 == fromCurrency || x.Item1 == toCurrency ||
                            x.Item2 == fromCurrency || x.Item2 == toCurrency))
                UpdateConfiguration(new List<Tuple<string, string, double>>
                {
                    new(fromCurrency, toCurrency, amount),
                });

            Tuple<string, string, double> second =
               ConversationTable
                   .FirstOrDefault(x =>
                       x.Item1 == fromCurrency && x.Item2 == toCurrency);

            return amount * second.Item3;

        }
    }
}
