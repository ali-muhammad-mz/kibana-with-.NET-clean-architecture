# Customer Microservice with MongoDB + Elasticsearch + Kibana

This project is a **Clean Architecture (Slim)** microservice built with **.NET Core**, **MongoDB**, and **Elasticsearch**, with visualization in **Kibana**. The microservice provides:

* **Create** and **Get** operations for customers
* **Automatic indexing into Elasticsearch** after create

---

## 🚀 Features

### **1. MongoDB as Primary Data Source**

All customer data is stored persistently in MongoDB. The service uses a standard collection named `customers`.

### **2. Elasticsearch Indexing**

Every customer inserted in MongoDB is also indexed in Elasticsearch.

### **3. Clean Architecture (Slim)**

For the sake of simplicity, DTOs are not used in this repository. The structure avoids excessive layering while maintaining good separation:

```
/Controllers
/Services
/Models
Program.cs
```

---

## 📁 Folder Structure

```
Controllers/
    CustomersController.cs

Services/
    CustomerService.cs

Models/
    Customer.cs

Program.cs
```

---

## 🐳 Docker Compose for Elasticsearch + Kibana

The repo contains the **docker-compose.yml** on root directory used to run Elasticsearch and Kibana as containers locally.

### **Run**

```bash
docker-compose up -d
```

### Access:

* **Elasticsearch:** [http://localhost:9200](http://localhost:9200)
* **Kibana:** [http://localhost:5601](http://localhost:5601)

---

## ▶️ Running the Microservice

### **1. Install Dependencies**

```bash
dotnet restore
```

### **2. Run the API**

```bash
dotnet run
```

---

## 📡 API Endpoints

### **GET /api/customers**

Returns all customers from MongoDB.

### **GET /api/customers/{id}**

Fetches a single customer.

### **POST /api/customers**

Creates a new customer, indexes it into Elasticsearch.

---

## 🧪 Testing the System

Try inserting a customer:

```json
{
  "id": "001",
  "fullName": "John Doe",
  "email": "test@email.com",
  "address": "test address",
}
```
---

## 📊 Using Kibana

1. Open Kibana → "Discover"
2. Select your index pattern (usually `customers*` or `customers`)
3. View indexed documents
4. Use Kibana Lens to visualize customer data
5. Select line graph
6. Drag and drop createdAt attribute to visualize
7. Save in a new dashboard
---

## 🏁 Conclusion

This microservice is a complete example of:

* Clean architecture
* MongoDB CRUD
* Elasticsearch indexing
* Kibana visualization