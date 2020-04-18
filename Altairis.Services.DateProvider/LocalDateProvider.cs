using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Services.DateProvider {
    public class LocalDateProvider : IDateProvider {

        public LocalDateProvider(DatePrecision precision = DatePrecision.Second) {
            this.Precision = precision;
        }

        public DatePrecision Precision { get; }

        public DateTime Now => DateTime.Now.TrimTo(this.Precision);

        public DateTime Today => DateTime.Today;

    }
}
