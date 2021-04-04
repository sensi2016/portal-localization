
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public enum RequestStatusEnum
    {
        Pending = 1,
        Verified = 2,
        Unverified = 3,
        Complete = 4,
        Cancellation = 5,
       // WaitingReSampling = 6,
        PartCompleted = 6
    }
}
