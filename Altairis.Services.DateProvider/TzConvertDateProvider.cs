using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Services.DateProvider {
    public class TzConvertDateProvider : IDateProvider {
        private TimeZoneInfo zoneInfo;

        public TzConvertDateProvider(string timeZoneId, DatePrecision precision = DatePrecision.Second) {
            this.TimeZoneId = timeZoneId ?? throw new ArgumentNullException(nameof(timeZoneId));
            this.Precision = precision;

            this.zoneInfo = TimeZoneInfo.FindSystemTimeZoneById(this.TimeZoneId);
        }

        public string TimeZoneId { get;  }

        public DatePrecision Precision { get;  }

        public DateTime Now => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.zoneInfo).TrimTo(this.Precision);

        public DateTime Today => TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, this.zoneInfo).Date;

    }
}
