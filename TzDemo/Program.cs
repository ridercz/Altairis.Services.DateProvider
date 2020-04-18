using System;
using Altairis.Services.DateProvider;

namespace TzDemo {
    class Program {
        static void Main(string[] args) {
            PrintTime("LocalDateProvider", new LocalDateProvider());
            PrintTime("UtcDateProvider", new UtcDateProvider());
            PrintTime("FixedDateProvider(new DateTime(2000, 1, 1))", new FixedDateProvider(new DateTime(2000, 1, 1)));
            PrintTime("TzConvertDateProvider(\"Central Europe Standard Time\")", new TzConvertDateProvider("Central Europe Standard Time"));
            PrintTime("TzConvertDateProvider(\"China Standard Time\")", new TzConvertDateProvider("China Standard Time"));
            Console.WriteLine("Values trimmed to seconds.");
        }

        static void PrintTime(string name, IDateProvider dateProvider) {
            var dt = dateProvider.Now;
            Console.WriteLine(name);
            Console.WriteLine("  {0,-40:o} ({1})", dt, dt.Kind);
            Console.WriteLine();
        }
    }
}
