using System;
using System.Collections.Generic;

namespace ConverterApplication
{
    public class ConversionList
    {
        public IEnumerable<Tuple<string, string, double>> ConversationTable =
            new List<Tuple<string, string, double>>
            {
                new ("USD","CAD",1.34),
                new ("CAD","GBP",0.58),
                new ("USD","EUR",0.86)
            };
    }
}