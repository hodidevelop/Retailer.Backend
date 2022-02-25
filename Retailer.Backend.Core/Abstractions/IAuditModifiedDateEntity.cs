using System;

namespace Retailer.Backend.Core.Abstractions
{
    public interface IAuditModifiedDateEntity
    {
        DateTime? ModifiedDate { get; set; }
    }
}
