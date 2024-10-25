using System;

namespace CNSMarketing.Domain.Entity.Common;

public class BaseEntity<ValueT> 
{
    public ValueT Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }

}
