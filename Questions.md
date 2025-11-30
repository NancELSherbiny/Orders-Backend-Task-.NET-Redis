# Task 5 — Short Questions
---

## 1️⃣ Redis vs SQL – Key Differences

| Redis                                      | SQL Server |
|--------------------------------------------|-----------------------------------------------------
| In-memory key–value store                  | Disk-based relational database |
| Extremely fast (microseconds)              | Slower (milliseconds) |
| Ideal for caching, sessions, rate-limiting | Ideal for transactional, relational data |
| No complex queries                         | Supports JOINs, indexing, constraints, transactions |
| Data may be volatile                       | Data persistence guaranteed |

➡ **Redis = speed**, **SQL = durability + complex queries**

---

## 2️⃣ When Not to Use Caching?

Avoid caching when:

- Data changes very frequently  
- Data is user-specific or security-sensitive  
- Freshness is critical (inventory, money balance)  
- Cache staleness may cause incorrect business logic  
- System is write-heavy and caching adds overhead

---

## 3️⃣ What If Redis Is Down?

A well-designed system should:

- Automatically **fall back to SQL Server**
- Continue functioning without Redis
- Log the failure but not crash
- Skip caching temporarily until Redis is available again

➡ **Redis is a performance layer, not a required system dependency.**

---

## 4️⃣ Optimistic vs Pessimistic Locking

### **Optimistic Locking**
- No locks during read
- Conflicts checked only on update
- Uses row versioning / timestamps
- Better scalability and performance
- Best for low-conflict systems

### **Pessimistic Locking**
- Locks the row immediately when read
- Prevents others from modifying it
- Can cause blocking and deadlocks
- Best for high-conflict scenarios

➡ **Optimistic = detect conflict**  
➡ **Pessimistic = prevent conflict**

---

## 5️⃣ Ways to Scale a .NET Backend

- **Horizontal scaling** (multiple API instances with load balancing)
- **Vertical scaling** (more CPU/RAM)
- **Redis caching** to reduce SQL load
- **Database optimization** (indexes, replication, sharding)
- **Async processing** with queues (RabbitMQ, Kafka, Azure Service Bus)
- **Microservices architecture**
- **Kubernetes / Docker Swarm** auto-scaling
- **CDN** for static content
- **API Gateway** for routing, throttling, rate-limiting

---
