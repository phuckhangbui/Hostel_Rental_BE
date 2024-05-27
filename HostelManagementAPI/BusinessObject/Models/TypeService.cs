namespace BusinessObject.Models
{
    public class TypeService
    {
        public int TypeServiceID { get; set; }
        public string? TypeName { get; set; }

        public IList<Services> Services { get; set; }
    }
}
