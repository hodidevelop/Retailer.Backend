using Retailer.Backend.Core.Abstractions;

using System;

namespace Retailer.Backend.Core.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
