using Retailer.Backend.Core.Abstractions;

using System;
using System.ComponentModel.DataAnnotations;

namespace Retailer.Backend.Core.Models.DAO.Base
{
    public abstract class EntityBase : IIdentifiedEntity, IAuditModifiedDateEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public EntityBase()
        { }
    }
}
