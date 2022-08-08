# Art exhibitions service

There are two simple microservices that talk through the Message Bus.

**Gallery Service** is used for managing (create and get) galleries. **Exhibition Service** allows you to manage exhibitions in a specific gallery.
When a new gallery is created, Gallery Service sends a message to the Message Bus. Exhibition Service is listening.

*There is also synchronous messaging between the services implemented with HttpClient, which is not that interesting.*

![test](https://github.com/IrinaChubarkina/Art-Exhibitions/blob/master/Diagram.png)

### In this project I did:
- Set up dockerfiles and K8S deployments
- Add Nginx Ingress controller as an API Gateway
- Use RabbitMQ asynchronous messaging between two services
- Deploy SQL Server persistent storage to K8S

### In this project I didn't:
- Follow architectural best practices much
- Add SQL Server for Exhibition Service (I left it In Memory)
- Separate database entities and domain models
- Configure database entities properly (such things as strings length etc)

