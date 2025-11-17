using MongoDB.Driver;
using Elastic.Clients.Elasticsearch;
using elasticsearch_kibana_microservice.Models;

namespace elasticsearch_kibana_microservice.Services
{
    public class CustomerService
    {
        private readonly IMongoCollection<Customer> _collection;
        private readonly ElasticsearchClient _es;

        public CustomerService(IMongoDatabase db, ElasticsearchClient es)
        {
            _collection = db.GetCollection<Customer>("customers");
            _es = es;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(string id)
        {
            return await _collection.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Customer customer)
        {
            try
            {
                customer.CreatedAt = DateTime.Now;

                await _collection.InsertOneAsync(customer);
                var response = await _es.IndexAsync(customer, i => i.Index("customers"));
                Console.WriteLine("Customer indexed in Elasticsearch.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to index customer: " + ex.Message);
            }
        }
    }
}
