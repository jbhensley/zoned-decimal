using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZonedDecimal;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            int valuesChecked = 0;

            // loop through a set of values and process
            int digitsLeft = 0, digitsRight = 0;
            for (decimal value = 0m; value < 10; value += 0.0001m)
            {
                int minDigitsLeft = value > 9 ? 2 : 1;
                for (digitsLeft = minDigitsLeft; digitsLeft < 20; digitsLeft++)
                {
                    for (digitsRight = 0; digitsRight < 10; digitsRight++)
                    {
                        CheckValue(value, ZonedDecimalConverter.GetZonedDecimal, ZonedDecimalConverter.GetDecimal);
                        valuesChecked++;
                        CheckValue(-value, ZonedDecimalConverter.GetZonedDecimal, ZonedDecimalConverter.GetDecimal);
                        valuesChecked++;
                    }
                }
            }

            stopwatch.Stop();
            Console.WriteLine($"ZonedDecimal checked {valuesChecked.ToString("#,###")} consecutive values in {stopwatch.Elapsed.TotalMilliseconds / 1000 / 60} sec for {valuesChecked / stopwatch.Elapsed.TotalMilliseconds} per ms");

            
            Console.WriteLine("Finished");
            Console.ReadLine();


            // local function to check values. can swap out functions to test different implementations
            void CheckValue(decimal valueToCheck, Func<decimal,int,int,ZonedDecimalConverter.RoundingOperation, string> getZonedDecimalFunc, Func<string,int,decimal> getDecimalFunc)
            {
                // check with truncate
                var zonedDecimal = getZonedDecimalFunc(valueToCheck, digitsLeft, digitsRight, ZonedDecimalConverter.RoundingOperation.Truncate);
                var returnValue = getDecimalFunc(zonedDecimal, digitsRight);
                decimal multiplier = (decimal)Math.Pow(10, digitsRight);
                if (returnValue != decimal.Truncate(valueToCheck * multiplier) / multiplier)
                    throw new Exception("Conversion failed");

                // check with round ToEven
                zonedDecimal = getZonedDecimalFunc(valueToCheck, digitsLeft, digitsRight, ZonedDecimalConverter.RoundingOperation.ToEven);
                returnValue = getDecimalFunc(zonedDecimal, digitsRight);
                if (returnValue != Math.Round(valueToCheck, digitsRight, MidpointRounding.ToEven))
                    throw new Exception("Conversion failed");

                // check with round AwayFromZero
                zonedDecimal = getZonedDecimalFunc(valueToCheck, digitsLeft, digitsRight, ZonedDecimalConverter.RoundingOperation.AwayFromZero);
                returnValue = getDecimalFunc(zonedDecimal, digitsRight);
                if (returnValue != Math.Round(valueToCheck, digitsRight, MidpointRounding.AwayFromZero))
                    throw new Exception("Conversion failed");
            }
        }
    }
}
