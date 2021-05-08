using SGRE.TSA.Models.Enums;

namespace SGRE.TSA.Models
{
    public class ScenarioOverView
    {
        //public int Id { get; set; }
       

        public int ScenarioId { get; set; }
        
        public int QuoteId { get; set; }

        public string WindfarmConfigurationId { get; set; }

        public int ScenarioNo { get; set; }

        public ScenarioTypes ScenarioType { get; set; }

        public int ItemId { get; set; }

        public ScenarioStatus Status { get; set; }

        public ScenarioProgress Progress { get; set; }

        public ScenarioOptions Options { get; set; }

        public bool StepProgress { get; set; }

        public ScenarioDesign ScenarioDesign { get; set; }
        public int ScenarioDesignId { get; set; }

        public int ScenarioCostKpiId { get; set; }
        public ScenarioCostsKpi ScenarioCostKpi { get; set; }


        public int TowerId { get; set; }

        public LoadsCluster loadsCluster { get; set; }
        public int LoadsClusterId { get; set; }

        public WtgCatalogue wtgCatalogue { get; set; }
        public int WtgCatalogueId { get; set; }

        public int TowerTypeId { get; set; }

        //public decimal? HubHeight { get; set; }
        public decimal LifeTime { get; set; }

        public decimal TowerWeight { get; set; }
        public decimal HubHeight { get; set; }
        public string DesignEvaluationRisk { get; set; }
        public decimal AepNominationGross { get; set; }
        public decimal AepOfferNet { get; set; }
        public decimal AepSignatureNet { get; set; }

    }
}