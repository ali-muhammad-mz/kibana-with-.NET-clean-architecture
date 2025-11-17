using MongoDB.Driver;
using Elastic.Clients.Elasticsearch;
using elasticsearch_kibana_microservice.Services;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
//  MONGO CONFIGURATION
// ----------------------------
var mongoConnection = builder.Configuration.GetValue<string>("Mongo:ConnectionString")
                     ?? "mongodb://localhost:27017";
var mongoDbName = builder.Configuration.GetValue<string>("Mongo:Database")
                 ?? "e-commerce";

var mongoClient = new MongoClient(mongoConnection);
var mongoDatabase = mongoClient.GetDatabase(mongoDbName);

// Register Mongo services
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddSingleton(mongoDatabase);

// ----------------------------
//  ELASTICSEARCH CONFIGURATION
// ----------------------------
var esUrl = builder.Configuration.GetValue<string>("Elasticsearch:Url") ?? "http://localhost:9200";

var esSettings = new ElasticsearchClientSettings(new Uri(esUrl))
    .DefaultIndex("default-index"); // optional, can specify per request

var esClient = new ElasticsearchClient(esSettings);

// register with DI
builder.Services.AddSingleton(esClient);

// ----------------------------
//  REGISTER SERVICES
// ----------------------------
builder.Services.AddSingleton<CustomerService>();

// ----------------------------
//  MVC / SWAGGER
// ----------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ----------------------------
//  MIDDLEWARE
// ----------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
