using System.Text.Json;

namespace SGRE.TSA.Models
{
    public class SstDesignRequest : Audit
    {
        public int Id { get; set; }
        public SstTower SstTower { get; set; }
        public int SstTowerId { get; set; }
        public RequestDesignEvaluation JsonServiceBusPayload { get; set; }
    }
}
