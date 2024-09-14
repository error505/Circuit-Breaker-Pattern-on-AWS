# Infrastructure Deployment

This folder contains the CloudFormation template (`cloudformation-template.yml`) and a GitHub Actions workflow (`deploy-cloudformation.yml`) to automate the deployment of AWS resources required for the Circuit Breaker Pattern on AWS.

## 📋 Prerequisites

- **AWS Account**: You must have an active AWS account.
- **AWS CLI**: Install the AWS CLI tool on your local machine.
- **GitHub Secrets**: Add the necessary secrets in your GitHub repository settings.

## 🔧 Deployment Steps

1. **Configure GitHub Secrets**

   Add the following secrets to your GitHub repository:

   - `AWS_ACCESS_KEY_ID`: Your AWS Access Key ID.
   - `AWS_SECRET_ACCESS_KEY`: Your AWS Secret Access Key.

2. **Deploy Infrastructure**

   The deployment is automated using GitHub Actions. To deploy the infrastructure:

   - **Trigger the GitHub Action**: Push changes to the `main` branch or manually trigger the `deploy-cloudformation.yml` workflow.

3. **Resources Deployed**

   The following AWS resources will be deployed:

   - AWS API Gateway
   - AWS Lambda Functions (`API`, `Retry`, `Fallback`)
   - Amazon RDS (PostgreSQL or MySQL)
   - Amazon ElastiCache (Redis)
   - AWS Amplify for Static Web App
   - Amazon CloudWatch for monitoring

## ⚙️ Configuration

- Modify the CloudFormation template (`cloudformation-template.yml`) as needed to customize the resource configurations.

## 📘 Usage

- Once deployed, the resources are ready for use. Refer to the respective README files in each component folder for further instructions.

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.
