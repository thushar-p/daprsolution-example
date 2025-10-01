# DaprSolution Example

This repository demonstrates a sample **.NET solution** using **Dapr** for service-to-service communication.  
It contains two projects under one solution:

1. `OrderService` – Sends requests to `TransactionService`.
2. `TransactionService` – Receives requests and maintains a list of transaction codes.

This setup uses **Dapr sidecars** to handle service to service invocation without caring the tight dependencies between them.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) or later
- [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (for Dapr sidecars)
- Git (for version control)

---

## Repository Setup

1. Clone the repository:

```bash
git clone https://github.com/thushar-p/daprsolution-example.git
```

---

## Run transaction service 

1. Open terminal in that folder
2. Run this cmd
```shell
dapr run --app-port 8080 --app-id transaction --app-protocol http --dapr-http-port 8081 -- dotnet run
```
3. Run swagger with localhost:8080/swagger

---

## Run order service

1. Open terminal in that folder
2. Run this cmd
```shell
dapr run --app-port 7000 --app-id order --app-protocol http --dapr-http-port 7001 -- dotnet run
```
3. Run swagger with localhost:7000/swagger

---
