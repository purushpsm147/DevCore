using System;

namespace SGRE.TSA.Models
{
    public abstract class Audit
    {
        public DateTimeOffset? RecordInsertDateTime { get; set; }
        public DateTimeOffset? LastModifiedDateTime { get; set; }
    }
}
