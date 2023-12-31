﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryVHGP.Core.Enums
{
    public enum SegmentModeEnum
    {
        StoreToHub = 1,
        HubToCus = 2,
        StoreToCus= 3
    }
    public enum SegmentStatusEnum
    {
        Unviable = 1,
        Viable = 2,
        Done = 3
    }
}
