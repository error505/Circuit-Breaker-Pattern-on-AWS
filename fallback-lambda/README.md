# Fallback Lambda Function

This folder contains the code for the Fallback Lambda function, which provides cached or alternative data when the main services are unavailable.

## 📋 Prerequisites

- **AWS Account**: You must have an active AWS account.
- **GitHub Secrets**: Ensure that the necessary secrets are added to your GitHub repository.

## 🔧 Deployment Instructions

1. **Configure GitHub Secrets**

   Add the following secrets to your GitHub repository:

   - `AWS_ACCESS_KEY_ID`: Your AWS Access Key ID.
   - `AWS_SECRET_ACCESS_KEY`: Your AWS Secret Access Key.
   - `AWS_LAMBDA_FALLBACK_FUNCTION_NAME`: The name of your Fallback Lambda function.

2. **Deploy Fallback Lambda Function**

   The deployment is automated using GitHub Actions. To deploy the Fallback Lambda function:

   - **Trigger the GitHub Action**: Push changes to the `main` branch or manually trigger the `deploy-fallback-lambda.yml` workflow.

## ⚙️ Configuration

- **Environment Variables**:

  Ensure that the following environment variables are set in the AWS Lambda console:

  - `REDIS_CONNECTION_STRING`: Connection string for Amazon ElastiCache (Redis).

## 📘 Usage

- The Fallback Lambda function is triggered by an HTTP POST request via the AWS API Gateway.
- It returns cached data or a default response if no cached data is available.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.
