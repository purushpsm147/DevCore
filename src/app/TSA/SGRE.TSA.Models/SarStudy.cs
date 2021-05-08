using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class SarStudy
    {
        public int Id { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Enter Data as DateTime")]
        public DateTimeOffset RecordInsertDTM { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Enter Data as DateTime")]
        public DateTimeOffset LastUpdateDTM { get; set; }
    }
}
