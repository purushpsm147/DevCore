namespace SGRE.TSA.Models
{
    public class AepInputJson
    {
        public int Id { get; set; }
        public string EstimationType { get; set; }
        public decimal HubHeight { get; set; }
        public decimal AEPIncrement { get; set; }
        public decimal AEPestimateGWh { get; set; }
    }
}