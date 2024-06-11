using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class VnPayReturnUrlDto
    {
        [Required]
        public string Url { get; set; }
        [Required]
        public string TnxRef { get; set; }
    }
}
