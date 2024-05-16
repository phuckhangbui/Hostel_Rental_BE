namespace BusinessObject
{
    public class TypeService
    {
        public int TypeServiceID { get; set; }
        public string? TypeName { get; set; }

        public IEnumerable<Service> Services { get; set; }
    }
}
