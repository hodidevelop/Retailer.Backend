using System;

namespace Retailer.Backend.Core.Abstractions
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}
