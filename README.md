# Circuit Breaker Pattern on AWS

This repository demonstrates the implementation of the **Circuit Breaker Pattern** on AWS, utilizing various AWS services to build a resilient, scalable, and fault-tolerant application architecture.

The architecture integrates multiple cloud patterns, including **Circuit Breaker**, **Cache-Aside**, **API Gateway Offloading**, **Retry Pattern**, and **Fallback Pattern** to handle transient faults, manage retries, and provide alternative responses in case of failures.

## 🏗️ Architecture Overview

The architecture consists of the following components:

1. **AWS API Gateway**: Acts as the main entry point for incoming client requests and routes them to the API Lambda function.
2. **API Lambda Function**: Implements the Circuit Breaker and Retry logic. It handles incoming client requests and delegates tasks to the appropriate Lambda functions.
3. **Retry Lambda Function**: Handles normal operations, such as reading/writing to the RDS database and updating the ElastiCache.
4. **Fallback Lambda Function**: Provides cached or alternative data when the main services are unavailable.
5. **Amazon ElastiCache (Redis)**: Caches data to reduce load on the RDS database and improve performance.
6. **Amazon RDS (PostgreSQL or MySQL)**: Stores data persistently for the application.
7. **AWS Amplify**: Hosts the React TypeScript front-end application, allowing users to interact with the API.
8. **Amazon CloudWatch**: Monitors and collects logs, metrics, and telemetry data from all services.

![image](https://github.com/user-attachments/assets/fef20c85-216e-43a3-9bdb-b3fd49170aec)

### 📊 Architectural Diagram

```mermaid
graph TD
    Client["Client (Web/Mobile App)"] -->|HTTP Requests| FrontEnd["AWS API Gateway"]
    FrontEnd -->|Routes Requests| ApiLambda["API Lambda Function"]
    
    ApiLambda -->|Implements Circuit Breaker + Retry| RetryLambda["Retry Lambda Function"]
    ApiLambda -->|Calls Fallback on Failure| FallbackLambda["Fallback Lambda Function"]

    RetryLambda -->|Read/Write Operations| RDS["Amazon RDS"]
    RetryLambda -->|Updates Cache| RedisCache["Amazon ElastiCache (Redis)"]
    RetryLambda -->|Handles Failure by Calling Fallback| FallbackLambda

    FallbackLambda -->|Provides Cached/Alternative Data| RedisCache
    FallbackLambda -->|Return Data to API| ApiLambda
    
    ApiLambda -->|Return Data| FrontEnd
    FrontEnd -->|Return Data| Client

    subgraph Monitoring
        CloudWatch["Amazon CloudWatch"]
    end

    ApiLambda -->|Logs + Metrics| CloudWatch
    RetryLambda -->|Logs + Metrics| CloudWatch
    FallbackLambda -->|Logs + Metrics| CloudWatch
```

## 📋 Prerequisites

To deploy and run this solution, you will need:

- **AWS Account**: An active AWS account.
- **AWS CLI**: Install the AWS CLI tool on your local machine.
- **GitHub Account**: A GitHub account to fork and clone the repository.
- **GitHub Secrets**: Required secrets added to your GitHub repository settings for automated deployment.

## 🚀 Getting Started

### 1. Fork and Clone the Repository

1. **Fork** this repository to your own GitHub account.
2. **Clone** the forked repository to your local machine:

   ```bash
   git clone https://github.com/<your-username>/circuit-breaker-pattern-aws.git
   cd circuit-breaker-pattern-aws
   ```

### 2. Set Up GitHub Secrets

Add the following secrets to your GitHub repository:

- **`AWS_ACCESS_KEY_ID`**: AWS Access Key ID.
- **`AWS_SECRET_ACCESS_KEY`**: AWS Secret Access Key.
- **`AWS_LAMBDA_API_FUNCTION_NAME`**: Name of the API Lambda function.
- **`AWS_LAMBDA_RETRY_FUNCTION_NAME`**: Name of the Retry Lambda function.
- **`AWS_LAMBDA_FALLBACK_FUNCTION_NAME`**: Name of the Fallback Lambda function.
- **`AWS_AMPLIFY_API_TOKEN`**: API token for deploying the AWS Amplify app.

### 3. Deploy the Infrastructure

1. **Deploy AWS Resources**:

   - The deployment of AWS resources is automated using GitHub Actions. To deploy the infrastructure:
   - Push changes to the `main` branch or manually trigger the `deploy-cloudformation.yml` workflow located in the **`infrastructure/.github/workflows/`** folder.

### 4. Deploy the Application Components

1. **API Lambda Function**:
   - Push changes to the `main` branch or manually trigger the `deploy-api-lambda.yml` workflow in the **`api-lambda/.github/workflows/`** folder.

2. **Retry and Fallback Lambda Functions**:
   - Push changes to the `main` branch or manually trigger the `deploy-retry-lambda.yml` and `deploy-fallback-lambda.yml` workflows in their respective folders.

3. **Static Web App**:
   - Push changes to the `main` branch or manually trigger the `deploy-static-web-app.yml` workflow in the **`static-web-app/.github/workflows/`** folder.

## 🧪 Testing the Solution

1. **Access the Static Web App**:
   - Open the URL of the AWS Amplify app to access the front-end application.

2. **Interact with the API**:
   - Use the input fields and buttons to perform operations. Observe the behavior when backend services are available or simulate failures to see the Circuit Breaker pattern in action.

3. **Monitor with Amazon CloudWatch**:
   - Use Amazon CloudWatch to monitor logs, metrics, and telemetry data for all services.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙌 Contributing

Contributions are welcome! Please open an issue or submit a pull request for any improvements or suggestions.

## 📞 Support

If you have any questions or need support, please open an issue or contact the repository maintainer.

## 🏷️ Tags

#AWS #CloudComputing #DevOps #CircuitBreakerPattern #Resilience #Microservices
