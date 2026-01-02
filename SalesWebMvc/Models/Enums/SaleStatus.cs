using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models
{
    public enum SaleStatus : int
    {
        PENDING,
        BILLED,
        CANCELED
    }
}
