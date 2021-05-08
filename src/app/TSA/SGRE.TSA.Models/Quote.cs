using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGRE.TSA.Models
{
    public class Quote : Audit
    {

        private string quotationId;

        public int Id { get; set; }
        public Proposal Proposal { get; set; }
        [Required]
        public int ProposalId { get; set; }

        [Required]
        public string QuotationId
        {
            get { return quotationId; }
            set { quotationId = value.Trim(); }
        }

        public string QuotationName { get; set; }

        [Required]
        public bool QuotationType { get; set; }

        [Required]
        public string OfferType { get; set; }

        public string QuoteStatus { get; set; }

        public DateTimeOffset? OfferSubmissionDate { get; set; }

        public string SarCode { get; set; }

        public ICollection<QuoteLine> QuoteLines { get; set; }

        public string Alert
        {
            get
            {
                if (QuoteLines.All(wtg => wtg.WindfarmSizeTrigger && wtg.QuantityTrigger))
                {
                    return "success";
                }
                else if (QuoteLines.All(wtg => !(wtg.WindfarmSizeTrigger || wtg.QuantityTrigger)))
                {
                    return "fail";
                }
                else
                {
                    return "partial fail";
                }
            }
        }
    }
}
