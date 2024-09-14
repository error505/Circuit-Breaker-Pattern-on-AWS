# Retry Lambda Function

This folder contains the code for the Retry Lambda function, which handles normal operations such as read/write actions to the RDS database and cache updates.

## 📋 Prerequisites

- **AWS Account**: You must have an active AWS account.
- **GitHub Secrets**: Ensure that the necessary secrets are added to your GitHub repository.

## 🔧 Deployment Instructions

1. **Configure GitHub Secrets**

   Add the following secrets to your GitHub repository:

   - `AWS_ACCESS_KEY_ID`: Your AWS Access Key ID.
   - `AWS_SECRET_ACCESS_KEY`: Your AWS Secret Access Key.
   - `AWS_LAMBDA_RETRY_FUNCTION_NAME`: The name of your Retry Lambda function.

2. **Deploy Retry Lambda Function**

   The deployment is automated using GitHub Actions. To deploy the Retry Lambda function:

   - **Trigger the GitHub Action**: Push changes to the `main` branch or manually trigger the `deploy-retry-lambda.yml` workflow.

## ⚙️ Configuration

- **Environment Variables**:

  Ensure that the following environment variables are set in the AWS Lambda console:

  - `REDIS_CONNECTION_STRING`: Connection string for Amazon ElastiCache (Redis).
  - `RDS_CONNECTION_STRING`: Connection string for Amazon RDS (PostgreSQL or MySQL).

## 📘 Usage

- The Retry Lambda function is triggered by an HTTP POST request via the AWS API Gateway.
- It performs the necessary operations and updates the cache if successful.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.
