namespace DTOs.Service
{
    public class UpdateServiceDto
    {
        public int ServiceID { get; set; }
        public int TypeServiceID { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
    }
}
