# Art exhibitions service

There are two simple microservices that talk through the Message Bus


### In this project I did:
- Set up dockerfiles and K8S deployments
- Add Nginx Ingress controller as an API Gateway
- Use RabbitMQ asynchronous messaging between two services
- Deploy SQL Server persistent storage to K8S

### In this project I didn't:
- Follow architectural best practices much
- Add SQL Server for the second service (I left it In Memory)
- Separate database entities and domain models
- Configure database entities (such things as string length etc)

