﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMagazine.Application.AppService.BackgroudApp
{
    public interface ISubscriptionTrackerService
    {
        Task TrackExpiredVendorsAsync();
    }
}
