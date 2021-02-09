using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Enums
{
    public enum RFStatus
    {
        Submitted,
        Approved,
        Completed,
        [Description("Not Complete")]
        NotCompleted,
        Cancelled,
        Rejected,
        Ongoing
    }
}
