using System;
using System.Collections.Generic;
using System.Text;

namespace Altairis.Services.DateProvider {
    public interface IDateProvider {
        DateTime Now { get; }

        DateTime Today { get; }
    }
}
