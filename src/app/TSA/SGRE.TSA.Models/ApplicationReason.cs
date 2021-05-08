using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SGRE.TSA.Models
{
    public class ApplicationReason : Audit
    {
        public int Id { get; set; }

        public string Reason { get; set; }
    }
}
