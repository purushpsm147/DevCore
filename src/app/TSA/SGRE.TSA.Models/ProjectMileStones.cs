using System;
using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class ProjectMileStones
    {

        public int Id { get; set; }

        public Project Project { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public MileStone MileStone { get; set; }
        [Required]
        public int MileStoneId { get; set; }

        [Required]
        public DateTime MileStoneDate { get; set; }

        public string MileStoneOfferStatus { get; set; }


    }
}
