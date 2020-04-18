using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altairis.Services.DateProvider {
    public class FixedDateProvider : IDateProvider {

        public FixedDateProvider(DateTime value) {
            this.Value = value;
        }

        public DateTime Value { get; }

        public DateTime Now => this.Value;

        public DateTime Today => this.Value.Date;

    }
}
