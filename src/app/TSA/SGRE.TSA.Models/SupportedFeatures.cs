using System;
using System.Collections.Generic;

namespace SGRE.TSA.Models
{
    public class SupportedFeatures
    {
        public ICollection<ApplicationModesFeatures> applicationModesFeatures { get; set; }
        public OtherFeatures otherFeatures { get; set; }
    }
    public class ApplicationModesFeatures
    {
        public int Id { get; set; }
        public int Mode { get; set; }
        public decimal RatingMW { get; set; }
        public bool Availability { get; set; }
    }

    public class OtherFeatures
    {
        public int Id { get; set; }
        public string Options { get; set; }
        public bool Activated { get; set; }
    }
  
    public class SegmentDimensionSummary
    {
        public int SstTowerId { get; set; }
        public Guid SstUuid { get; set; }
        public decimal HubHeight { get; set; }
        public decimal TowerHeight { get; set; }
        public int NumberOfSections { get; set; }
    }

    public class SegmentDimensionTable
    {
        public Guid SstUuid { get; set; }

        public int SectionNumber { get; set; }

        public string SectionType { get; set; }

        public decimal SectionLength { get; set; }

        public int OuterDiameterTop { get; set; }

        public int OuterDiameterBottom { get; set; }

        public decimal MaxPlateThickness { get; set; }

        public decimal WeightPlates { get; set; }

        public int WeightFlangesTop { get; set; }

        public int WeightFlangesBottom { get; set; }

        public decimal TransportWeight { get; set; }
    }


}