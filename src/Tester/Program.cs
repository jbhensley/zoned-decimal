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
            var stopwatch = Stopwatch.StartNew();
            var valuesChecked = UnitTests.LongConversionTest();
            stopwatch.Stop();

            Console.WriteLine($"Checked {valuesChecked.ToString("#,###")} consecutive values in {stopwatch.Elapsed.TotalMilliseconds / 1000 / 60} sec for {valuesChecked / stopwatch.Elapsed.TotalMilliseconds} per ms");


            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
