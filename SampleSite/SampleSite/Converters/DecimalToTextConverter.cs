using System;

namespace SampleSite.Converters
{
    public class DecimalToTextConverter : INumberToTextConverter<decimal>
    {
        private static string[] Digits = {"","one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};
        private static string[] Teens = { "","eleven", "twelve", "thriteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] Tens = {"","ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"};
            
        public string Convert(decimal amount)
        {
            if (amount < 1m || amount > 1000000m)
            {
                throw new ArgumentOutOfRangeException("amount", "The value must be between $1 and $1M");
            }

            if (amount == 1000000m)
            {
                return "one million dollars"; //This is the top value and so no other processing is required.
            }

            var convertedDollars = string.Empty;
            var convertedCents = string.Empty;

            var cents = (amount - Math.Truncate(amount)) * 100; //ectract the cents portion

            if (cents > 0)
            {
                convertedCents = string.Format("{0} {1}", ConvertBlock(cents), cents == 1 ? "cent": "cents"); // singular or plural
            }

            var blockHundreds = Math.Truncate(amount % (decimal)Math.Pow(10, 3)); //move through each block
            if (blockHundreds > 0)
            {
                convertedDollars = string.Format("{0} {1}", ConvertBlock(blockHundreds), convertedDollars).Trim();
            }

            var blockThousands = Math.Truncate(amount / (decimal)Math.Pow(10, 3) % (decimal)Math.Pow(10, 6)); //move through each block
            if (blockThousands > 0)
            {
                convertedDollars = string.Format("{0} thousand {1}", ConvertBlock(blockThousands), convertedDollars).Trim();
            }

            if (!string.IsNullOrEmpty(convertedDollars) && !string.IsNullOrEmpty(convertedCents))
            {
                return string.Format("{0} dollars and {1}", convertedDollars, convertedCents);
            }

            if ( !string.IsNullOrEmpty(convertedCents))
            {
                return string.Format("{0}", convertedCents);
            }

            if (!string.IsNullOrEmpty(convertedDollars))
            {
                return string.Format("{0} dollars", convertedDollars);
            }

            return convertedDollars;
        }

        private string ConvertBlock(decimal value)
        {
            string result;

            var hundreds = (int) Math.Truncate(value/100);

            var remainder = (int) value%100;

            if (remainder > 10 && remainder < 20)
            {
                result = string.Format("{0}", Teens[remainder - 10]);
            }
            else
            {
                var tens = remainder/10;

                var units = remainder%10;

                result = string.Format("{0} {1}", Tens[tens], Digits[units]); 
            }

            result = result.Trim(); //strip any excess spaces from no tens or units.

            if (hundreds > 0 && !string.IsNullOrEmpty(result))
            {
                result = string.Format("{0} hundred and {1}", Digits[hundreds], result);
            }
            else if (hundreds > 0)
            {
                result = string.Format("{0} hundred", Digits[hundreds]);
            }

            return result.Trim();
        }
    }
}