using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altairis.Services.DateProvider {
    public static class DateTimeExtensions {

        public static DateTime TrimTo(this DateTime value, DatePrecision precision) {
            switch (precision) {
                case DatePrecision.Minute:
                    return value.TrimToMinutes();
                case DatePrecision.Second:
                    return value.TrimToSeconds();
                default:
                    return value;
            }
        }

        public static DateTimeOffset TrimTo(this DateTimeOffset value, DatePrecision precision) {
            switch (precision) {
                case DatePrecision.Minute:
                    return value.TrimToMinutes();
                case DatePrecision.Second:
                    return value.TrimToSeconds();
                default:
                    return value;
            }
        }

        public static TimeSpan TrimTo(this TimeSpan value, DatePrecision precision) {
            switch (precision) {
                case DatePrecision.Minute:
                    return value.TrimToMinutes();
                case DatePrecision.Second:
                    return value.TrimToSeconds();
                default:
                    return value;
            }
        }

        public static DateTime TrimToSeconds(this DateTime value) => value.AddTicks(-value.Ticks % TimeSpan.TicksPerSecond);

        public static DateTimeOffset TrimToSeconds(this DateTimeOffset value) => value.AddTicks(-value.Ticks % TimeSpan.TicksPerSecond);

        public static TimeSpan TrimToSeconds(this TimeSpan value) => value.Subtract(new TimeSpan(value.Ticks % TimeSpan.TicksPerSecond));

        public static DateTime TrimToMinutes(this DateTime value) => value.AddTicks(-value.Ticks % TimeSpan.TicksPerMinute);

        public static DateTimeOffset TrimToMinutes(this DateTimeOffset value) => value.AddTicks(-value.Ticks % TimeSpan.TicksPerMinute);

        public static TimeSpan TrimToMinutes(this TimeSpan value) => value.Subtract(new TimeSpan(value.Ticks % TimeSpan.TicksPerMinute));

    }

    public enum DatePrecision {
        Native = 0,
        Second = 1,
        Minute = 2
    }

}
