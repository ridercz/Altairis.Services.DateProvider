using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Services.DateProvider {
    public class UtcDateProvider : IDateProvider {

        public UtcDateProvider(DatePrecision precision = DatePrecision.Second) {
            this.Precision = precision;
        }

        public DatePrecision Precision { get; }

        public DateTime Now => DateTime.UtcNow.TrimTo(this.Precision);

        public DateTime Today => DateTime.UtcNow.Date;

    }
}
