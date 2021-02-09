using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory_Management_System.Enums
{
    //Manual Transaction / Auto Transaction
    public enum ITStatus
    {
        Auto,
        PendingApproval,
        Approved,
        Rejected
    }
}
