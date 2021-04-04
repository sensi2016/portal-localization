using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public enum SamplingStatusEnum
    {
        Normal=1,
        Broken=2,
        ClotSamples=3,
        UnmirkedSampleIdentitfier=4,
        SpecicmenImpregnatedWithInterstitialFluid=5,
        TheSampleCollectedInTheWrongTube=6
    }
}
