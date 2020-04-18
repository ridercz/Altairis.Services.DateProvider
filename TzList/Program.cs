using System;
using System.Linq;

namespace TzList {
    class Program {
        static void Main(string[] args) {
            var systemZones = TimeZoneInfo.GetSystemTimeZones().Select(x => new { x.Id, x.DisplayName });
            var maxIdLength = systemZones.Max(x => x.Id.Length);

            foreach (var item in systemZones) {
                Console.WriteLine(string.Join(string.Empty, item.Id.PadRight(maxIdLength+2), item.DisplayName));
            }
        }
    }
}
