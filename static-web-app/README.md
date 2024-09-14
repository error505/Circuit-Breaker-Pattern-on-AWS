# Static Web App

This folder contains the React TypeScript front-end application that interacts with the AWS API Gateway and showcases the Circuit Breaker Pattern.

## 📋 Prerequisites

- **AWS Account**: You must have an active AWS account.
- **GitHub Secrets**: Ensure that the necessary secrets are added to your GitHub repository.

## 🔧 Deployment Instructions

1. **Configure GitHub Secrets**

   Add the following secrets to your GitHub repository:

   - `AWS_AMPLIFY_API_TOKEN`: The API token for deploying the AWS Amplify app.

2. **Deploy Static Web App**

   The deployment is automated using GitHub Actions. To deploy the Static Web App:

   - **Trigger the GitHub Action**: Push changes to the `main` branch or manually trigger the `deploy-static-web-app.yml` workflow.

## 📘 Usage

- Once deployed, open the AWS Amplify URL.
- Use the input field and button to interact with the AWS API Gateway.
- The front-end will display responses or errors based on the API behavior.

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](../LICENSE) file for details.
