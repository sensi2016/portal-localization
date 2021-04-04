using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{ 
    public enum ReceptionServiceStatusEnum
    {
        WaitingSampling=1,
        WaitForAnswer=2,
        Answered = 3,
        WaitingReSampling=4,
        UnAnswered=5,
        WatingForPrint=6,
        Printed=5
    }
}
