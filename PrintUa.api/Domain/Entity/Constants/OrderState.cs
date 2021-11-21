using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entity
{
    public enum OrderState
    {
        New = 0,
        InProgress = 1,
        Ready = 2,
        Sent = 3,
        Taken = 4
    }
}