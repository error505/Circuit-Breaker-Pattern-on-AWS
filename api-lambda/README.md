# API Lambda Function

This folder contains the code for the API Lambda function, which implements the Circuit Breaker and Retry logic to handle incoming client requests and delegate tasks to the appropriate Lambda functions.

## 📋 Prerequisites

- **AWS Account**: You must have an active AWS account.
- **GitHub Secrets**: Ensure that the necessary secrets are added to your GitHub repository.

## 🔧 Deployment Instructions

1. **Configure GitHub Secrets**

   Add the following secrets to your GitHub repository:

   - `AWS_ACCESS_KEY_ID`: Your AWS Access Key ID.
   - `AWS_SECRET_ACCESS_KEY`: Your AWS Secret Access Key.
   - `AWS_LAMBDA_API_FUNCTION_NAME`: The name of your API Lambda function.

2. **Deploy API Lambda Function**

   The deployment is automated using GitHub Actions. To deploy the API Lambda function:

   - **Trigger the GitHub Action**: Push changes to the `main` branch or manually trigger the `deploy-api-lambda.yml` workflow.

## ⚙️ Configuration

- **Environment Variables**:

  Ensure that the following environment variables are set in the AWS Lambda console:

  - `RETRY_FUNCTION_URL`: The URL of the Retry Lambda function.
  - `FALLBACK_FUNCTION_URL`: The URL of the Fallback Lambda function.

## 📘 Usage

- The API Lambda function handles client requests at the AWS API Gateway endpoint.
- It manages circuit breaker logic to call either the Retry or Fallback function based on the availability of the backend services.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.
