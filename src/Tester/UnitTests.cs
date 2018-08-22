using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ZonedDecimal;

namespace Tester
{
    public class UnitTests
    {
        [Fact]
        public void GetZonedDecimal_Returns_Valid_Positives()
        {
            Assert.Equal("0219{", ZonedDecimalConverter.GetZonedDecimal(21.90m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219A", ZonedDecimalConverter.GetZonedDecimal(21.91m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219B", ZonedDecimalConverter.GetZonedDecimal(21.92m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219C", ZonedDecimalConverter.GetZonedDecimal(21.93m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219D", ZonedDecimalConverter.GetZonedDecimal(21.94m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219E", ZonedDecimalConverter.GetZonedDecimal(21.95m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219F", ZonedDecimalConverter.GetZonedDecimal(21.96m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219G", ZonedDecimalConverter.GetZonedDecimal(21.97m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219H", ZonedDecimalConverter.GetZonedDecimal(21.98m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219I", ZonedDecimalConverter.GetZonedDecimal(21.99m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
        }

        [Fact]
        public void GetZonedDecimal_Returns_Valid_Negatives()
        {
            Assert.Equal("0219}", ZonedDecimalConverter.GetZonedDecimal(-21.90m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219J", ZonedDecimalConverter.GetZonedDecimal(-21.91m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219K", ZonedDecimalConverter.GetZonedDecimal(-21.92m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219L", ZonedDecimalConverter.GetZonedDecimal(-21.93m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219M", ZonedDecimalConverter.GetZonedDecimal(-21.94m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219N", ZonedDecimalConverter.GetZonedDecimal(-21.95m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219O", ZonedDecimalConverter.GetZonedDecimal(-21.96m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219P", ZonedDecimalConverter.GetZonedDecimal(-21.97m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219Q", ZonedDecimalConverter.GetZonedDecimal(-21.98m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
            Assert.Equal("0219R", ZonedDecimalConverter.GetZonedDecimal(-21.99m, 3, 2, ZonedDecimalConverter.RoundingOperation.Truncate));
        }        

        [Fact]
        public void GetDecimal_Returns_Valid_Positives()
        {
            Assert.Equal(21.90m, ZonedDecimalConverter.GetDecimal("0219{", 2));
            Assert.Equal(21.91m, ZonedDecimalConverter.GetDecimal("0219A", 2));
            Assert.Equal(21.92m, ZonedDecimalConverter.GetDecimal("0219B", 2));
            Assert.Equal(21.93m, ZonedDecimalConverter.GetDecimal("0219C", 2));
            Assert.Equal(21.94m, ZonedDecimalConverter.GetDecimal("0219D", 2));
            Assert.Equal(21.95m, ZonedDecimalConverter.GetDecimal("0219E", 2));
            Assert.Equal(21.96m, ZonedDecimalConverter.GetDecimal("0219F", 2));
            Assert.Equal(21.97m, ZonedDecimalConverter.GetDecimal("0219G", 2));
            Assert.Equal(21.98m, ZonedDecimalConverter.GetDecimal("0219H", 2));
            Assert.Equal(21.99m, ZonedDecimalConverter.GetDecimal("0219I", 2));
        }

        [Fact]
        public void GetDecimal_Returns_Valid_Negatives()
        {
            Assert.Equal(-21.90m, ZonedDecimalConverter.GetDecimal("0219}", 2));
            Assert.Equal(-21.91m, ZonedDecimalConverter.GetDecimal("0219J", 2));
            Assert.Equal(-21.92m, ZonedDecimalConverter.GetDecimal("0219K", 2));
            Assert.Equal(-21.93m, ZonedDecimalConverter.GetDecimal("0219L", 2));
            Assert.Equal(-21.94m, ZonedDecimalConverter.GetDecimal("0219M", 2));
            Assert.Equal(-21.95m, ZonedDecimalConverter.GetDecimal("0219N", 2));
            Assert.Equal(-21.96m, ZonedDecimalConverter.GetDecimal("0219O", 2));
            Assert.Equal(-21.97m, ZonedDecimalConverter.GetDecimal("0219P", 2));
            Assert.Equal(-21.98m, ZonedDecimalConverter.GetDecimal("0219Q", 2));
            Assert.Equal(-21.99m, ZonedDecimalConverter.GetDecimal("0219R", 2));
        }

        [Fact]
        public void ZonedDecimalConverter_GetZonedDecimal_GetDecimal_Returns_Original_Value()
        {
            LongConversionTest();
        }

        public static int LongConversionTest()
        {   
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

            return valuesChecked;


            // local function to check values. can swap out functions to test different implementations
            void CheckValue(decimal valueToCheck, Func<decimal, int, int, ZonedDecimalConverter.RoundingOperation, string> getZonedDecimalFunc, Func<string, int, decimal> getDecimalFunc)
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
