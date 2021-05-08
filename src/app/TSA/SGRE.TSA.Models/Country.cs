using System.ComponentModel.DataAnnotations;

namespace SGRE.TSA.Models
{
    public class Country
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Country Name is Required", MinimumLength = 3)]
        public string CountryName { get; set; }
        public Region Region { get; set; }
        public int RegionId { get; set; }
        public Currency Currency { get; set; }
        public int CurrencyId { get; set; }
    }
}
