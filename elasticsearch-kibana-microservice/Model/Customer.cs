namespace elasticsearch_kibana_microservice.Models
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
